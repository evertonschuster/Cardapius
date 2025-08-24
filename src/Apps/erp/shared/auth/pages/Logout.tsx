import React, { useEffect } from 'react'
import { useAuth } from '../AuthProvider';

export const Logout = () => {
    const { signout } = useAuth();

    useEffect(() => {
        signout();
    }, []);

    return null;
}
