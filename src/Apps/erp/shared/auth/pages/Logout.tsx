import { useEffect, useState } from 'react'
import { useAuth } from '../AuthProvider';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { LoadProgressPage } from '../components/LoadProgressPage';
import { AuthErrorDetails } from '../types/AuthErrorDetails';

export const Logout = () => {
    const [error, setError] = useState<AuthErrorDetails | null>(null);
    const { signout } = useAuth();

    useEffect(() => {
        signout()
            .then((error) => {
                if (error) {
                    setError(error);
                }
            }).catch((err) => {
                console.error('Error during signout callback:', err);
            });
    }, []);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signout} />;
    }
    return <LoadProgressPage title='Processando logout...' />
}
