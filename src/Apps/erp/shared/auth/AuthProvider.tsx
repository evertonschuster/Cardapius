import React, {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';
import { UserManager, User } from 'oidc-client-ts';

interface AuthContextValue {
  user: User | null;
  signin: () => Promise<void>;
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
        authority: import.meta.env.VITE_OIDC_AUTHORITY || '',
        redirect_uri: window.location.origin + '/callback',
        silent_redirect_uri: window.location.origin + '/silent-renew',
        post_logout_redirect_uri: window.location.origin + '/login',
        scope: import.meta.env.VITE_OIDC_SCOPE || 'openid profile',
        response_type: 'code',
      }),
    [],
  );

  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    userManager.getUser().then(setUser);
    userManager.events.addUserLoaded(setUser);
    userManager.events.addUserUnloaded(() => setUser(null));
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

  const signin = () => userManager.signinRedirect();
  const signout = () => {
    localStorage.setItem('logout', Date.now().toString());
    setUser(null);
    return userManager.signoutRedirect();
  };
  const refresh = () => userManager.signinSilent();
  const hasRole = (role: string) => {
    const roles = (user?.profile as any)?.roles as string[] | undefined;
    return roles?.includes(role) ?? false;
  };

  const value = useMemo(
    () => ({ user, signin, signout, refresh, hasRole }),
    [user],
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

