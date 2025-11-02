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

interface AuthProviderProps {
  client?: AuthClient;
}

export const AuthProvider: React.FC<React.PropsWithChildren<AuthProviderProps>> = ({
  children,
  client,
}) => {
  const navigate = useNavigate();
  const location = useLocation();

  const authClient = useMemo(() => client ?? createAuthClient(), [client]);

  const [isLoading, setIsLoading] = useState(true);
  const [user, setUser] = useState<AuthUser | null>(null);
  const [error, setError] = useState<AuthErrorDetails | null>(null);

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

  useEffect(() => {
    let mounted = true;
    setIsLoading(true);

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

    authClient.getUser().then((u) => {
      try {
        if (!mounted) return;
        setUser(u);
      }
      finally {
        setIsLoading(false);
      }
    });

    authClient.events.addUserLoaded(onUserLoaded);
    authClient.events.addUserUnloaded(onUserUnloaded);
    authClient.events.addSilentRenewError(onSilentRenewError);

    return () => {
      mounted = false;
      authClient.events.removeUserLoaded(onUserLoaded);
      authClient.events.removeUserUnloaded(onUserUnloaded);
      authClient.events.removeSilentRenewError(onSilentRenewError);
    };
  }, [authClient, refresh]);


  const isAuthenticated = !!user && user.expired === false;
  const value = useMemo<AuthContextValue>(
    () => {
      return ({
        user,
        isLoading,
        isAuthenticated,
        signin,
        signinCallback,
        signout,
        refresh,
        hasRole,
        error,
      })
    },
    [user, isLoading, signin, signinCallback, signout, refresh, hasRole, error, isAuthenticated]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
};
