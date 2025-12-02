import { useEffect, useState } from 'react'
import { useAuth } from '../AuthProvider';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { AuthErrorDetails } from '../types/AuthErrorDetails';
import { useNavigate } from 'react-router-dom';

export const Callback = () => {

    const navigate = useNavigate();
    const { signinCallback, signin, user, isAuthenticated } = useAuth();
    const [error, setError] = useState<AuthErrorDetails | null>(null);

    useEffect(() => {
        let active = true;

        if (isAuthenticated) {
            return;
        }

        signinCallback()
            .then((response) => {
                if (!active) return;

                if (response?.error) {
                    setError(response.error);
                }
                if (response?.redirectTo) {
                    navigate(response.redirectTo);
                }
            }).catch((err) => {
                console.error('Error during signin callback:', err);
            });

        return () => {
            active = false;
        }

    }, [isAuthenticated, signinCallback, navigate]);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signin} />;
    }

    return <LoadProgressPage title='Carregando informações...' />
}
