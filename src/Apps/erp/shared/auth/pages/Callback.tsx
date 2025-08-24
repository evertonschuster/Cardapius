import React, { useCallback, useEffect, useRef } from 'react'
import { useAuth } from '../AuthProvider';

export const Callback = () => {
    const { signinCallback } = useAuth();

    const signin = useCallback(async () => {
        console.log('Calling signinCallback...');
        await signinCallback();
    }, []);

    useEffect(() => {
        signin();
    }, []);

    return <div>Loading...</div>
}
