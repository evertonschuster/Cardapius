import { Box } from '@mui/material';
import { AppThemeContext } from '@shared/theme';
import { ReactNode } from 'react';

export const Layout = ({ children }: { children: ReactNode }) => {

  return (
    <AppThemeContext>
      <Box
        sx={{
          display: "flex",
          flex: 1,
          minHeight: "100vh",
          minWidth: "100vw",
          bgcolor: "background.default",
        }}
      >
        {children}
      </Box>
    </AppThemeContext >
  );
};
