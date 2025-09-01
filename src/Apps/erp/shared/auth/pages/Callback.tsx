import { useEffect } from 'react'
import { useAuth } from '../AuthProvider';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';

export const Callback = () => {
    const { signinCallback, signin, error, isLoading, user } = useAuth();

    useEffect(() => {
        if (isLoading || user) {
            return;
        }
        signinCallback();
    }, []);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signin} />;
    }
    return <LoadProgressPage title='Carregando informações...' />
}
