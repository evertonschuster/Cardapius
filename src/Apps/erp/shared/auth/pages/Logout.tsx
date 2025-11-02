import React, { useEffect } from 'react'
import { useAuth } from '../AuthProvider';
import { ProcessErrorDetails } from '../components/ProcessErrorDetails';
import { LoadProgressPage } from '../components/LoadProgressPage';

export const Logout = () => {
    const { signout, error } = useAuth();

    useEffect(() => {
        signout();
    }, []);

    if (error) {
        return <ProcessErrorDetails details={error} onRetry={signout} />;
    }
    return <LoadProgressPage title='Processando logout...' />
}
