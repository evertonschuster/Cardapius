import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { ReactNode, useMemo } from "react";
import { ThemeContext, ThemeMode } from "./ThemeContext";
import { usePersistentState } from "@shared/hooks/usePersistentState";



export const getTheme = (mode: ThemeMode) =>
  createTheme({
    palette: {
      mode,
      primary: {
        main: "#7C3AED", // roxinho
      },
      background: {
        default: mode === "dark" ? "#050816" : "#F5F5F7",
        paper: mode === "dark" ? "#050816" : "#FFFFFF",
      },
    },

  });



export const AppThemeContext = ({ children }: { children: ReactNode }) => {
  const [mode, setMode] = usePersistentState<ThemeMode>("themeMode", "dark");
  const theme = useMemo(() => getTheme(mode), [mode]);
  const contextValue = useMemo(() => ({ mode, setMode }), [mode, setMode]);

  return (
    <ThemeContext.Provider value={contextValue}>
      <ThemeProvider theme={theme} >

        <CssBaseline />
        {children}

      </ThemeProvider>
    </ThemeContext.Provider>
  )
}
