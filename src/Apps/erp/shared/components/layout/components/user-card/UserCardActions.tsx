import * as React from "react";
import { IconButton, Stack } from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import SettingsIcon from "@mui/icons-material/Settings";
import { useNavigate } from "react-router-dom";
import { useCallback } from "react";

type UserCardActionsProps = {
  onOpenSettings: () => void;
};

export const UserCardActions: React.FC<UserCardActionsProps> = ({onOpenSettings}) => {

  const navigate = useNavigate();

  const handleSignout = useCallback(() => {
    navigate("logout")
  }, [navigate]);

  return (
    <Stack direction="row" spacing={1} alignItems="center" justifyContent="flex-end" >
      <IconButton
        size="small"
        onClick={onOpenSettings}
        sx={{
          border: (theme) =>
            `1px solid ${theme.palette.divider}`,
        }}
      >
        <SettingsIcon fontSize="small" />
      </IconButton>

      <IconButton
        size="small"
        onClick={handleSignout}
        sx={{
          border: (theme) =>
            `1px solid ${theme.palette.divider}`,
        }}
      >
        <LogoutIcon fontSize="small" />
      </IconButton>
    </Stack>
  );
};
