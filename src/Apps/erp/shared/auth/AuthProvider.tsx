import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';
import { AuthContextValue } from './types/AuthContextValue';
import authService from './services/authService';
import { LoadProgressPage } from './components/LoadProgressPage';
import { AuthState } from './services/authClient';

export const AuthContext = createContext<AuthContextValue | undefined>(undefined);
const rolPropName = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

export const AuthProvider: React.FC<React.PropsWithChildren> = ({
  children,
}) => {

  const auth = useMemo(() => authService, []);
  const [authInitilizaed, setAuthInitilizaed] = useState(false);
  const [authState, setAuthState] = useState<AuthState | null>(null);

  useEffect(() => {
    const unsubscribeUserLoaded = auth.addUserLoaded((user) => {
      console.log('User loaded:', user);
      setAuthState((prevState) => ({
        ...prevState,
        user: user,
        isAuthenticated: !!user && !user.expired,
      }));
    });

    auth.initAsync().then((state) => {
      setAuthInitilizaed(true);
      setAuthState(state);
    });

    return () => {
      unsubscribeUserLoaded();
    }

  }, []);

  const hasRole = useCallback((role: string | string[]) => {
    const roles = (authState?.user?.profile as any)?.[rolPropName] as string[] | undefined;

    if (Array.isArray(role)) {
      return role.every(r => roles?.includes(r));
    }
    return roles?.includes(role) ?? false;
  }, [authState]);

  const value = useMemo<AuthContextValue>(() => {
    return ({
      user: authState?.user,
      isAuthenticated: !!authState?.isAuthenticated,
      signin: authService.signinAsync.bind(authService),
      signinCallback: authService.signinCallbackAsync.bind(authService),
      signout: authService.signoutAsync.bind(authService),
      hasRole: hasRole,
    } as AuthContextValue)
  }, [authState, hasRole]);

  if (!authInitilizaed) {
    return (
      <LoadProgressPage title="Iniciando a aplicação..." />
    );
  }


  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>);
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
};
