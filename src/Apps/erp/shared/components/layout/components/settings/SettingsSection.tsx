import React from "react";
import { Box, Typography } from "@mui/material";

export interface SettingsSectionProps {
  label: string;
  children: React.ReactNode;
}

export const SettingsSection: React.FC<SettingsSectionProps> = ({
  label,
  children,
}) => {
  return (
    <Box>
      <Typography
        variant="caption"
        sx={{
          fontWeight: 500,
          mb: 1,
          display: "block",
        }}
      >
        {label}
      </Typography>

      {children}
    </Box>
  );
};
