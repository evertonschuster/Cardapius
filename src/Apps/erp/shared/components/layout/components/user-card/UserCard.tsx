import * as React from "react";
import { Avatar, Box, IconButton, Stack, Tooltip } from "@mui/material";
import { UserCardHeader } from "./UserCardHeader";
import { UserCardActions } from "./UserCardActions";
import { useAuth } from "@shared/auth";
import { SettingsDrawer } from "../settings/SettingsDrawer";
import { useState } from "react";
import SettingsIcon from "@mui/icons-material/Settings";
import LogoutIcon from "@mui/icons-material/Logout";
import { useNavigate } from "react-router-dom";

type UserCardProps = {
  collapsed?: boolean;
};

export const UserCard: React.FC<UserCardProps> = ({ collapsed = false }) => {

  const { user } = useAuth();
  const [showSettingDrawer, setShowSettingDrawer] = useState(false);
  const navigate = useNavigate();

  const name = user?.profile.name ?? "Desconhecido";
  const email = user?.profile.email ?? "desconhecido";
  const planLabel = "Starter"
  const avatarUrl = "/static/images/avatars/avatar_2.png"

  return (
    <Box
      sx={{
        px: collapsed ? 0.5 : 1.5,
        py: 1.5,
        m: .3,
        borderRadius: 2,
        marginTop: "auto",
        bgcolor: (theme) => theme.palette.mode === "dark" ? "#111827" : "#FFFFFF",
        border: (theme) => theme.palette.mode === "dark" ? "1px solid #1F2937" : `1px solid ${theme.palette.divider}`,
      }}
    >
      {collapsed ? (
        <Stack spacing={1.5} alignItems="center">
          <Tooltip title={name} placement="right">
            <Avatar src={avatarUrl} alt={name} sx={{ width: 40, height: 40 }} />
          </Tooltip>

            <Stack direction="row" spacing={1}>
              <Tooltip title="Configurações" placement="top">
              <IconButton
                size="small"
                onClick={() => setShowSettingDrawer(true)}
                sx={{
                  border: (theme) =>
                    `1px solid ${theme.palette.mode === "dark"
                      ? "#374151"
                      : theme.palette.divider
                    }`,
                }}
              >
                <SettingsIcon fontSize="small" />
              </IconButton>
              </Tooltip>

            <Tooltip title="Sair" placement="top">
              <IconButton
                size="small"
                onClick={() => navigate("logout")}
                sx={{
                  border: (theme) =>
                    `1px solid ${theme.palette.mode === "dark"
                      ? "#374151"
                      : theme.palette.divider
                    }`,
                }}
              >
                <LogoutIcon fontSize="small" />
              </IconButton>
            </Tooltip>
          </Stack>
        </Stack>
      ) : (
        <Stack spacing={1.5}>
          <UserCardHeader
            name={name}
            email={email}
            avatarUrl={avatarUrl}
            planLabel={planLabel}
          />

          <UserCardActions
            onOpenSettings={() => setShowSettingDrawer(true)}
          />
        </Stack>
      )}

      <SettingsDrawer
        open={showSettingDrawer}
        onClose={() => setShowSettingDrawer(false)}
      />
    </Box>
  );
};
