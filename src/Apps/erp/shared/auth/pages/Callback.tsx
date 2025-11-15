import { useEffect } from 'react'
import { useAuth } from '../AuthProvider';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';

export const Callback = () => {
    const { signinCallback, signin, error, isLoading, user, isAuthenticated } = useAuth();

    useEffect(() => {
        if (!isLoading && !isAuthenticated && !user) {
            console.log('User not found, redirecting to login...');
            signinCallback();
            return;
        }
    }, [isLoading, isAuthenticated, signinCallback, user]);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signin} />;
    }

    return <LoadProgressPage title='Carregando informações...' />
}
