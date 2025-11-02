import { useEffect } from 'react';
import { useAuth } from '../AuthProvider';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { LoadProgressPage } from '../components/LoadProgressPage';

export const Login = () => {
  const { signin, error } = useAuth();

  useEffect(() => {
    signin();
  }, []);

   if (error) {
          return <ProcessErrorDetails details={error} onRetry={signin} />;
      }
      return <LoadProgressPage title='Carregando informações...' />
};
