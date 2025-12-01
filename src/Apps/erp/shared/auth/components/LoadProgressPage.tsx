import { Box, LinearProgress, Typography } from '@mui/material'
import React from 'react'

interface LoadProgressPageProps {
  title: string
}

export const LoadProgressPage = ({ title }: LoadProgressPageProps) => {
  return (
    <Box sx={{
      height: '100vh',
      width: '100vw',
      alignItems: 'center',
      alignContent: 'center',
      alignSelf: 'center',
    }}>
      <LinearProgress
        content='Loading...'
        title='Loading...' />
      <Typography variant="subtitle1" align="center">
        {title}
      </Typography>
    </Box>
  )
}
