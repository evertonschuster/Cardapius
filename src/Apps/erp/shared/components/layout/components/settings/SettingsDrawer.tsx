// src/components/settings/SettingsDrawer.tsx
import React from "react";
import { Drawer, Box, Divider } from "@mui/material";
import { SettingsHeader } from "./SettingsHeader";
import { ModeSection } from "./ModeSection";

export type ThemeMode = "light" | "dark";

export interface SettingsDrawerProps {
  open: boolean;
  onClose: () => void;
}

export const SettingsDrawer: React.FC<SettingsDrawerProps> = ({
  open,
  onClose,
}) => {

  return (
    <Drawer
      anchor="right"
      open={open}
      onClose={onClose}
      slotProps={{
        paper: {
          sx: {
            width: 360,
            bgcolor: "background.default",
            color: "text.primary",
          },
        },
      }}
    >
      <Box
        sx={{
          height: "100%",
          display: "flex",
          flexDirection: "column",
        }}
      >
        <SettingsHeader onClose={onClose} />

        <Divider />

        <Box
          sx={{
            p: 2.5,
            display: "flex",
            flexDirection: "column",
            gap: 3,
          }}
        >
          <ModeSection />
        </Box>
      </Box>
    </Drawer>
  );
};
