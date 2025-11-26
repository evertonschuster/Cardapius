import { useEffect, useState } from 'react';
import { useAuth } from '../AuthProvider';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { AuthErrorDetails } from '../types/AuthErrorDetails';

export const Login = () => {
  const { signin } = useAuth();
  const [error, setError] = useState<AuthErrorDetails | null>(null);


  useEffect(() => {
    signin()
      .then((error) => {
        if (error) {
          setError(error);
        }
      }).catch((err) => {
        console.error('Error during signin:', err);
      });
  }, []);

  if (error) {
    return <ProcessErrorDetails details={error} onRetry={signin} />;
  }
  return <LoadProgressPage title='Processando informações de login...' />
};
