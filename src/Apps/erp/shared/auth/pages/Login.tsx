import { useEffect } from 'react';
import { useAuth } from '../AuthProvider';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { LoadProgressPage } from '../components/LoadProgressPage';

export const Login = () => {
  const { signin, error } = useAuth();

  useEffect(() => {
    console.log('Redirecting to login...');
    signin();
  }, []);

   if (error) {
          return <ProcessErrorDetails details={error} onRetry={signin} />;
      }
      return <LoadProgressPage title='Processando informações de login...' />
};
