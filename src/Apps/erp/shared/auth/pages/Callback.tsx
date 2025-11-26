import { useEffect, useState } from 'react'
import { useAuth } from '../AuthProvider';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { AuthErrorDetails } from '../types/AuthErrorDetails';

export const Callback = () => {

    const [error, setError] = useState<AuthErrorDetails | null>(null);
    const { signinCallback, signin, user, isAuthenticated } = useAuth();

    useEffect(() => {
        signinCallback()
            .then((error) => {
                if (error) {
                    setError(error);
                }
            }).catch((err) => {
                console.error('Error during signin callback:', err);
            });

    }, [isAuthenticated, signinCallback, user]);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signin} />;
    }

    return <LoadProgressPage title='Carregando informações...' />
}
