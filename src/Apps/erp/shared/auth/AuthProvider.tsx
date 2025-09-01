import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';
import { UserManager, User, WebStorageStateStore } from 'oidc-client-ts';
import { useLocation, useNavigate } from 'react-router-dom';
import { OidcService } from './services/oidcService';
import { AuthErrorDetails } from './types/AuthErrorDetails';
import { AuthContextValue } from './types/AuthContextValue';


export const AuthContext = createContext<AuthContextValue | undefined>(
  undefined,
);

const INACTIVITY_TIMEOUT_MS = 10 * 60 * 1000;

export const AuthProvider: React.FC<React.PropsWithChildren> = ({
  children,
}) => {
  const userManager = useMemo(
    () =>
      new UserManager({
        client_id: import.meta.env.VITE_OIDC_CLIENT_ID || '',
        client_secret: import.meta.env.VITE_OIDC_CLIENT_SECRET || '',
        authority: import.meta.env.VITE_OIDC_AUTHORITY || '',
        redirect_uri: window.location.origin + '/callback',
        silent_redirect_uri: window.location.origin + '/silent-renew',
        post_logout_redirect_uri: window.location.origin + '/login',
        scope: import.meta.env.VITE_OIDC_SCOPE || 'openid profile',
        response_type: 'code',
        revokeTokensOnSignout: true,
        automaticSilentRenew: true,
        userStore: new WebStorageStateStore({ store: window.localStorage, prefix: 'oidc' })
      }),
    [],
  );

  const navigate = useNavigate();
  const location = useLocation();
  const [isLoading, setIsLoading] = useState(false);
  const [user, setUser] = useState<User | null>(null);
  const [error, setError] = useState<AuthErrorDetails | null>(null);


  const buildAuthErrorDetails = useCallback(async (err: unknown): Promise<AuthErrorDetails> => {
    const anyErr = err as any;

    const url = OidcService.getOidcParamsFromUrl();
    const code = anyErr?.error ?? url.error ?? (anyErr?.name === "TypeError" ? "network_error" : null);
    const description = anyErr?.error_description ?? url.error_description ?? anyErr?.message ?? null;
    const errorUri = anyErr?.error_uri ?? url.error_uri ?? null;
    const state = anyErr?.state ?? null;
    const traceId = state?.traceId ?? sessionStorage.getItem("oidc:lastTraceId") ?? null;
    const requestId = state?.requestId ?? null;

    return {
      title:
        code === "login_required"
          ? "Sua sessão expirou"
          : "Não foi possível processar sua solicitação",
      description,
      code,
      errorUri,
      traceId,
      requestId,
      timestamp: new Date().toISOString(),
      authority: userManager.settings.authority,
      clientId: userManager.settings.client_id ?? null,
      redirectUri: userManager.settings.redirect_uri ?? null,
    };
  }, []);

  const getUrlAtual = useCallback(
    () => `${location.pathname}${location.search ?? ''}${location.hash ?? ''}`,
    [location],
  );

  const signin = useCallback(async () => {
    try {
      setError(null);
      setIsLoading(true);
      const returnTo = getUrlAtual();
      sessionStorage.setItem('returnTo', returnTo); // fallback
      await userManager.signinRedirect({ state: { returnTo } });
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, getUrlAtual, userManager]);

  const signinCallback = useCallback(async () => {
    try {
      setError(null);
      setIsLoading(true);
      const loggedUser = await userManager.signinRedirectCallback();
      setUser(loggedUser);

      const state = (loggedUser?.state as any) || {};
      const returnTo: string = state?.returnTo || sessionStorage.getItem('returnTo') || '/';

      if (returnTo.indexOf('/login') === 0 || returnTo.indexOf('/callback') === 0 || returnTo.indexOf('/logout') === 0) {
        await navigate('/', { replace: true });
        return;
      }

      sessionStorage.removeItem('returnTo');
      await navigate(returnTo, { replace: true });
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, navigate, userManager]);

  const signout = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      setUser(null);
      sessionStorage.setItem('returnTo', '/');
      await userManager.signoutRedirect({ state: { returnTo: '/' } });
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, userManager]);

  const refresh = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      await userManager.signinSilent();
    } catch (err: any) {
      setError(await buildAuthErrorDetails(err));
    } finally {
      setIsLoading(false);
    }
  }, [buildAuthErrorDetails, userManager]);

  const hasRole = useCallback(
    (role: string) => {
      const roles = (user?.profile as any)?.roles as string[] | undefined;
      return roles?.includes(role) ?? false;
    },
    [user],
  );

  useEffect(() => {
    userManager.getUser().then((user) => {
      setUser(user);
      setIsLoading(false);
    });
    userManager.events.addUserLoaded((user) => {
      setUser(user);
      setIsLoading(false);
    });
    userManager.events.addUserUnloaded(() => {
      setUser(null);
      setIsLoading(false);
    });
    userManager.events.addAccessTokenExpiring(refresh);
    userManager.events.addSilentRenewError(() => {
      setIsLoading(false);
    });

    return () => {
      userManager.events.removeUserLoaded(() => setUser(null));
      userManager.events.removeUserUnloaded(() => setUser(null));
    };
  }, [userManager]);

  useEffect(() => {
    let timeout: NodeJS.Timeout;
    const reset = () => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        alert('Sessão expirada por inatividade');
        userManager.signoutRedirect();
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
  }, [userManager]);

  useEffect(() => {
    const handler = (e: StorageEvent) => {
      if (e.key === 'logout') {
        userManager.signoutRedirect();
      }
    };
    window.addEventListener('storage', handler);
    return () => window.removeEventListener('storage', handler);
  }, [userManager]);

  const value = useMemo<AuthContextValue>(
    () => ({ user, isLoading, signin, signinCallback, signout, refresh, hasRole, error }),
    [user, isLoading, signin, signinCallback, signout, refresh, hasRole, error],
  );

  return (
    <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
};

