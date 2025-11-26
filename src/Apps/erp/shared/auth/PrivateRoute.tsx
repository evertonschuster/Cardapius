import React, { useState } from 'react';
import { Outlet } from 'react-router-dom';
import { useAuth } from './AuthProvider';
import { LoadProgressPage } from './components/LoadProgressPage';
import { ProcessErrorDetails } from './components/ProcessErrorDetails';
import { AuthErrorDetails } from './types/AuthErrorDetails';

interface PrivateRouteProps {
  roles?: string[];
  children?: React.ReactNode;
}

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ roles, children }) => {
  const { signin, hasRole, isAuthenticated } = useAuth();
  const [error, setError] = useState<AuthErrorDetails | null>(null);

  if (error) {
    return <ProcessErrorDetails details={error} onRetry={signin} />
  }

  if (!isAuthenticated) {
    signin().then((error) => {
      console.log('Signin returned', error);
      if (error) {
        setError(error);
      }
    }).catch((err) => {
      console.error('Error during signin callback:', err);
    });
    return <LoadProgressPage title='Aguardando autenticação...' />
  }

  if (roles && !roles.every(hasRole)) {
    return (
      <div>Acesso negado</div>
    );
  }

  return <>{children ?? <Outlet />}</>;
};
