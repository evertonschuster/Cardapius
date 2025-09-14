import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useRef,
  useState,
} from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { OidcService } from './services/oidcService';
import { AuthErrorDetails } from './types/AuthErrorDetails';
import { AuthContextValue } from './types/AuthContextValue';
import { AuthClient, createAuthClient } from './services/authClient';
import { AuthUser } from './types/AuthUser';

export const AuthContext = createContext<AuthContextValue | undefined>(undefined);

const INACTIVITY_TIMEOUT_MS = 10 * 60 * 1000;

interface AuthProviderProps {
  client?: AuthClient;
}

export const AuthProvider: React.FC<React.PropsWithChildren<AuthProviderProps>> = ({
  children,
  client,
}) => {
  const navigate = useNavigate();
  const location = useLocation();

  // --- Auth client wrapper (silent renew automático + sessionStorage) ---
  const authClient = useMemo(() => client ?? createAuthClient(), [client]);

  const [isLoading, setIsLoading] = useState(false);
  const [user, setUser] = useState<AuthUser | null>(null);
  const [error, setError] = useState<AuthErrorDetails | null>(null);

  // trava simples para evitar concorrência no refresh manual
  const isRefreshingRef = useRef(false);

  const buildAuthErrorDetails = useCallback(
    async (err: unknown): Promise<AuthErrorDetails> => {
      const anyErr = err as any;
      const url = OidcService.getOidcParamsFromUrl();

      const code =
        anyErr?.error ??
        url.error ??
        (anyErr?.name === 'TypeError' ? 'network_error' : null);

      const description =
        anyErr?.error_description ?? url.error_description ?? anyErr?.message ?? null;

      const errorUri = anyErr?.error_uri ?? url.error_uri ?? null;
      const state = anyErr?.state ?? null;
      const traceId = state?.traceId ?? sessionStorage.getItem('oidc:lastTraceId') ?? null;
      const requestId = state?.requestId ?? null;

      return {
        title: code === 'login_required' ? 'Sua sessão expirou' : 'Não foi possível processar sua solicitação',
        description,
        code,
        errorUri,
        traceId,
        requestId,
        timestamp: new Date().toISOString(),
        authority: authClient.settings.authority,
        clientId: authClient.settings.client_id ?? null,
        redirectUri: authClient.settings.redirect_uri ?? null,
      };
    },
    [authClient]
  );

  const getUrlAtual = useCallback(
    () => `${location.pathname}${location.search ?? ''}${location.hash ?? ''}`,
    [location]
  );

  const signin = useCallback(async () => {
    try {
      setError(null);
      setIsLoading(true);
      const returnTo = getUrlAtual();
      sessionStorage.setItem('returnTo', returnTo); // fallback pós-login
      await authClient.signinRedirect({ state: { returnTo } });
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, getUrlAtual, authClient]);

  const signinCallback = useCallback(async () => {
    try {
      setError(null);
      setIsLoading(true);
      const loggedUser = await authClient.signinRedirectCallback();
      setUser(loggedUser);

      const state = (loggedUser?.state as any) || {};
      const returnTo: string =
        state?.returnTo || sessionStorage.getItem('returnTo') || '/';

      sessionStorage.removeItem('returnTo');

      // evita loop em rotas de auth
      if (
        returnTo.startsWith('/login') ||
        returnTo.startsWith('/callback') ||
        returnTo.startsWith('/logout')
      ) {
        navigate('/', { replace: true });
      } else {
        navigate(returnTo, { replace: true });
      }
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, navigate, authClient]);

  const signout = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      setUser(null);
      sessionStorage.setItem('returnTo', '/');
      await authClient.signoutRedirect({ state: { returnTo: '/' } });
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, authClient]);

  // refresh manual (mantido para fallback/log), com trava
  const refresh = useCallback(async () => {
    if (isRefreshingRef.current) return;
    isRefreshingRef.current = true;
    try {
      setError(null);
      await authClient.signinSilent();
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      isRefreshingRef.current = false;
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, authClient]);

  const hasRole = useCallback(
    (role: string) => {
      const roles = (user?.profile as any)?.roles as string[] | undefined;
      return roles?.includes(role) ?? false;
    },
    [user]
  );

  // --- Eventos do cliente de autenticação (com mesmas referências e cleanup correto) ---
  useEffect(() => {
    let mounted = true;

    const onUserLoaded = (u: AuthUser) => {
      if (!mounted) return;
      setUser(u);
      setIsLoading(false);
    };
    const onUserUnloaded = () => {
      if (!mounted) return;
      setUser(null);
      setIsLoading(false);
    };
    const onSilentRenewError = () => {
      if (!mounted) return;
      setIsLoading(false);
    };
    // opcional: log/telemetria
    const onAccessTokenExpiring = () => {
      // NÃO chamar signinSilent aqui (automaticSilentRenew já faz)
      // Se quiser forçar um fallback:
      // refresh();
    };

    authClient.getUser().then((u) => {
      if (!mounted) return;
      setUser(u);
      setIsLoading(false);
    });

    authClient.events.addUserLoaded(onUserLoaded);
    authClient.events.addUserUnloaded(onUserUnloaded);
    authClient.events.addSilentRenewError(onSilentRenewError);
    authClient.events.addAccessTokenExpiring(onAccessTokenExpiring);

    return () => {
      mounted = false;
      authClient.events.removeUserLoaded(onUserLoaded);
      authClient.events.removeUserUnloaded(onUserUnloaded);
      authClient.events.removeSilentRenewError(onSilentRenewError);
      authClient.events.removeAccessTokenExpiring(onAccessTokenExpiring);
    };
  }, [authClient, refresh]);

  // --- Inatividade: desloga após X ms sem interação ---
  useEffect(() => {
    let timeout: ReturnType<typeof setTimeout>;

    const reset = () => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        alert('Sessão expirada por inatividade');
        authClient.signoutRedirect();
      }, INACTIVITY_TIMEOUT_MS);
    };

    document.addEventListener('mousemove', reset);
    document.addEventListener('keydown', reset);
    reset();

    return () => {
      document.removeEventListener('mousemove', reset);
      document.removeEventListener('keydown', reset);
      clearTimeout(timeout);
    };
  }, [authClient]);

  // --- Single logout multi-abas (storage event) ---
  useEffect(() => {
    const handler = (e: StorageEvent) => {
      if (e.key === 'logout') {
        authClient.signoutRedirect();
      }
    };
    window.addEventListener('storage', handler);
    return () => window.removeEventListener('storage', handler);
  }, [authClient]);

  const value = useMemo<AuthContextValue>(
    () => ({
      user,
      isLoading,
      isAuthenticated: !!user,
      signin,
      signinCallback,
      signout,
      refresh,
      hasRole,
      error,
    }),
    [user, isLoading, signin, signinCallback, signout, refresh, hasRole, error]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
};
