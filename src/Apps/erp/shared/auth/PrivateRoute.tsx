import React, { useEffect } from 'react';
import { Outlet } from 'react-router-dom';
import { useAuth } from './AuthProvider';

interface PrivateRouteProps {
  roles?: string[];
  children?: React.ReactNode;
}

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ roles, children }) => {
  const { user, isLoading, signin, hasRole } = useAuth();

  useEffect(() => {
    if (!user && !isLoading) {
      signin();
    }
  }, [user, signin]);

  if (!user) {
    return <div>Aguardando autenticação...</div>;
  }

  if (roles && !roles.every(hasRole)) {
    return <div>Acesso negado</div>;
  }

  return <>{children ?? <Outlet />}</>;
};
