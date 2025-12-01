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
        signinCallback()
            .then((response) => {
                if (response?.error) {
                    setError(response.error);
                }
                if (response?.redirectTo) {
                    navigate(response.redirectTo);
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
