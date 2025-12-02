import { createContext, useContext } from "react";

export type ThemeMode = "light" | "dark";

export interface ThemeContextState {
    mode: ThemeMode;
    setMode: (mode: ThemeMode) => void;
}

export const ThemeContext = createContext<ThemeContextState | undefined>(undefined);

export const useThemeMode = (): ThemeContextState => {
    const ctx = useContext(ThemeContext);
    if (!ctx) {
        throw new Error("useThemeMode must be used inside ThemeContext provider");
    }
    return ctx;
};

