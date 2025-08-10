import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from './AuthContext';

interface Props {
  roles?: string[];
}

export const PrivateRoute = ({ roles = [] }: Props) => {
  const { accessToken, roles: userRoles, loading } = useAuth();

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!accessToken) {
    return <Navigate to="/login" replace />;
  }

  if (roles.length && !roles.some((r) => userRoles.includes(r))) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
};
