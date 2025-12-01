import React, { MouseEvent } from "react";
import { ToggleButtonGroup, ToggleButton } from "@mui/material";
import { SettingsSection } from "./SettingsSection";
import { ThemeMode } from "./SettingsDrawer";
import { useThemeMode } from "@shared/theme/ThemeContext";

export interface ModeSectionProps {
}

export const ModeSection: React.FC<ModeSectionProps> = () => {

  const { mode, setMode } = useThemeMode();

  const handleChange = (_event: MouseEvent<HTMLElement>, newMode: ThemeMode | null
  ) => {
    if (!newMode) return;
    setMode(newMode);
  };

  return (
    <SettingsSection label="MODE">
      <ToggleButtonGroup
        value={mode}
        exclusive
        onChange={handleChange}
        fullWidth
        sx={{
          "& .MuiToggleButton-root": {
            flex: 1,
            textTransform: "none",
            py: 1.2,
            borderRadius: 1,
            borderColor: "divider",
          },
        }}
      >
        <ToggleButton value="light">Light</ToggleButton>
        <ToggleButton value="dark">Dark</ToggleButton>
      </ToggleButtonGroup>
    </SettingsSection>
  );
};
