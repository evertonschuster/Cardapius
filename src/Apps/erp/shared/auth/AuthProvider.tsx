import React, {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';
import { UserManager, User, WebStorageStateStore } from 'oidc-client-ts';
import { useLocation, useNavigate } from 'react-router-dom';
import { set } from 'react-hook-form';

interface AuthContextValue {
  user: User | null;
  isLoading: boolean;
  signin: () => Promise<void>;
  signinCallback: () => Promise<void>;
  signout: () => Promise<void>;
  refresh: () => Promise<void>;
  hasRole: (role: string) => boolean;
}

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
        userStore: new WebStorageStateStore({ store: window.localStorage, prefix: 'oidc' })
      }),
    [],
  );

  const navigate = useNavigate();
  const location = useLocation();
  const [isLoading, setIsLoading] = useState(true);
  const [user, setUser] = useState<User | null>(null);

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
    userManager.events.addAccessTokenExpiring(() => {
      userManager.signinSilent();
    });
    userManager.events.addAccessTokenExpired(() => {
      alert('Sessão expirada');
      userManager.signinRedirect();
    });

    return () => {
      userManager.events.removeUserLoaded(setUser);
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

  const getUrlAtual = () =>
    `${location.pathname}${location.search ?? ''}${location.hash ?? ''}`;


  const signin = () => {
    const returnTo = getUrlAtual();
    sessionStorage.setItem('returnTo', returnTo); // fallback
    return userManager.signinRedirect({ state: { returnTo } });
  };

  const signinCallback = async () => {
    const loggedUser = await userManager.signinRedirectCallback();
    setUser(loggedUser);
    setIsLoading(false);

    const state = (loggedUser?.state as any) || {};
    const returnTo: string = state?.returnTo || sessionStorage.getItem('returnTo') || '/';
    console.log('Navigating to:', returnTo, state?.returnTo, sessionStorage.getItem('returnTo') );

    if (returnTo.indexOf('/login') === 0) {
      await navigate("/", { replace: true });
      return;
    }

    sessionStorage.removeItem('returnTo');
    await navigate(returnTo, { replace: true });
  };

  const signout = () => {
    setUser(null);
    sessionStorage.setItem('returnTo', "/")
    return userManager.signoutRedirect({state: { returnTo: '/' } });
  };
  const refresh = async () => { await userManager.signinSilent() };
  const hasRole = (role: string) => {
    const roles = (user?.profile as any)?.roles as string[] | undefined;
    return roles?.includes(role) ?? false;
  };

  const value = useMemo<AuthContextValue>(
    () => ({ user, isLoading, signin, signinCallback, signout, refresh, hasRole }),
    [user, isLoading],
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

