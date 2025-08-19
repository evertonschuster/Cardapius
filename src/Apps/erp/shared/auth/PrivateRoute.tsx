import React, { useEffect } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from './AuthProvider';

interface PrivateRouteProps {
  roles?: string[];
  children?: React.ReactNode;
}

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ roles, children }) => {
  const { user, signin, hasRole } = useAuth();

  useEffect(() => {
    if (!user) {
      signin();
    }
  }, [user, signin]);

  if (!user) {
    return null;
  }

  if (roles && !roles.every(hasRole)) {
    return <Navigate to="/login" replace />;
  }

  return <>{children ?? <Outlet />}</>;
};
