import React from "react";
import { Box, Typography, IconButton } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

export interface SettingsHeaderProps {
  onClose: () => void;
}

export const SettingsHeader: React.FC<SettingsHeaderProps> = ({ onClose }) => {
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-between",
        px: 2.5,
        py: 2,
      }}
    >
      <Typography variant="subtitle1" fontWeight={600}>
        Configurações do sistema
      </Typography>

      <IconButton size="small" onClick={onClose} aria-label="Close settings">
        <CloseIcon fontSize="small" />
      </IconButton>
    </Box>
  );
};
