import { useEffect } from 'react';
import { useAuth } from '../AuthProvider';

export const Login = () => {
  const { signin } = useAuth();

  useEffect(() => {
    console.log('Calling signin...');
    signin();
  }, []);

  return null;
};
