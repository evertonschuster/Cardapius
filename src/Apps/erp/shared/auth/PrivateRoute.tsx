import React, { useEffect } from 'react';
import { Outlet } from 'react-router-dom';
import { useAuth } from './AuthProvider';
import { LoadProgressPage } from './components/LoadProgressPage';
import { ProcessErrorDetails } from './components/ProcessErrorDetails';

interface PrivateRouteProps {
  roles?: string[];
  children?: React.ReactNode;
}

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ roles, children }) => {
  const { user, signin, hasRole, isLoading, error } = useAuth();

  useEffect(() => {
    if (isLoading || user || error) {
      return;
    }

    signin();
  }, []);

  if (error) {
    return <ProcessErrorDetails details={error} onRetry={signin} />
  }

  if (!user || isLoading) {
    return <LoadProgressPage title='Aguardando autenticação...' />
  }

  if (roles && !roles.every(hasRole)) {
    return <div>Acesso negado</div>;
  }

  return <>{children ?? <Outlet />}</>;
};
