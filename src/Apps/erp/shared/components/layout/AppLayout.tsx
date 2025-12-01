import { Box } from '@mui/material'
import React from 'react'
import { Outlet } from 'react-router-dom'
import { Sidebar } from './components/sidebar/Sidebar'

export const AppLayout: React.FC<React.PropsWithChildren> = ({ children }) => {
    return (
        <>
            <Sidebar />
            <Box component="main">
                <Outlet />
            </Box>
        </>
    )
}
