import React, { createContext, useContext, useEffect, useState, ReactNode } from 'react';
import { authService } from './infrastructure/authService';

interface AuthContextData {
  accessToken: string | null;
  refreshToken: string | null;
  roles: string[];
  loading: boolean;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
  refresh: () => Promise<void>;
}

const AuthContext = createContext<AuthContextData | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [accessToken, setAccessToken] = useState<string | null>(null);
  const [refreshToken, setRefreshToken] = useState<string | null>(null);
  const [roles, setRoles] = useState<string[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const init = async () => {
      const storedAccess = localStorage.getItem('accessToken');
      const storedRefresh = localStorage.getItem('refreshToken');
      if (storedAccess && storedRefresh) {
        setAccessToken(storedAccess);
        setRefreshToken(storedRefresh);
        const session = await authService.loadSession(storedAccess);
        setRoles(session.roles);
      }
      setLoading(false);
    };
    init();
  }, []);

  const login = async (username: string, password: string) => {
    const res = await authService.login(username, password);
    setAccessToken(res.accessToken);
    setRefreshToken(res.refreshToken);
    setRoles(res.roles);
    localStorage.setItem('accessToken', res.accessToken);
    localStorage.setItem('refreshToken', res.refreshToken);
  };

  useEffect(() => {
    if (!refreshToken) return;
    const interval = setInterval(() => {
      refresh();
    }, 5 * 60 * 1000);
    return () => clearInterval(interval);
  }, [refreshToken]);

  const refresh = async () => {
    if (!refreshToken) return;
    const res = await authService.refreshToken(refreshToken);
    setAccessToken(res.accessToken);
    setRefreshToken(res.refreshToken);
    setRoles(res.roles);
    localStorage.setItem('accessToken', res.accessToken);
    localStorage.setItem('refreshToken', res.refreshToken);
  };

  const logout = () => {
    setAccessToken(null);
    setRefreshToken(null);
    setRoles([]);
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  };

  return (
    <AuthContext.Provider value={{ accessToken, refreshToken, roles, loading, login, logout, refresh }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
