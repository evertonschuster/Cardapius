import { Box } from '@mui/material'
import React from 'react'
import { Outlet } from 'react-router-dom'
import { Sidebar } from './components/sidebar/Sidebar'

export const AppLayout: React.FC<React.PropsWithChildren> = ({ children }) => {
    return (
        <Box
            sx={{
                display: 'flex',
                minHeight: '100vh',
            }}
        >
            <Sidebar />
            <Box component="main" sx={{ flex: 1 }}>
                {children ?? <Outlet />}
            </Box>
        </Box>
    )
}
