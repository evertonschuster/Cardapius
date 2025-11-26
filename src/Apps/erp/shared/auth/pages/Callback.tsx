import { useEffect, useState } from 'react'
import { useAuth } from '../AuthProvider';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { AuthErrorDetails } from '../types/AuthErrorDetails';

export const Callback = () => {

    const [error, setError] = useState<AuthErrorDetails | null>(null);
    const { signinCallback, signin, user, isAuthenticated } = useAuth();

    useEffect(() => {
        let isMounted = true;

        const processCallback = async () => {
            if (isAuthenticated || user) {
                return;
            }

            try {
                const error = await Promise.resolve(signinCallback());
                if (error && isMounted) {
                    setError(error);
                }
            } catch (err) {
                if (isMounted) {
                    console.error('Error during signin callback:', err);
                }
            }
        };

        processCallback();

        return () => {
            isMounted = false;
        }

    }, [isAuthenticated, signinCallback, user]);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signin} />;
    }

    return <LoadProgressPage title='Carregando informações...' />
}
