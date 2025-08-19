import { useEffect } from 'react';
import { useAuth } from './AuthProvider';

export const Login = () => {
  const { signin } = useAuth();

  useEffect(() => {
    signin();
  }, [signin]);

  return null;
};
