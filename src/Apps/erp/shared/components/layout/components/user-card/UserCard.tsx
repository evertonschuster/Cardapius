import * as React from "react";
import { Box, Stack } from "@mui/material";
import { UserCardHeader } from "./UserCardHeader";
import { UserCardActions } from "./UserCardActions";
import { useAuth } from "@shared/auth";
import { SettingsDrawer } from "../settings/SettingsDrawer";
import { useState } from "react";

export const UserCard: React.FC = ({ }) => {

  const { user } = useAuth();
  const [showSettingDrawer, setShowSettingDrawer] = useState(false);

  const name = user?.profile.name ?? "Desconhecido";
  const email = user?.profile.email ?? "desconhecido";
  const planLabel = "Starter"
  const avatarUrl = "/static/images/avatars/avatar_2.png"

  return (
    <Box
      sx={{
        px: 1.5,
        py: 1.5,
        m: .3,
        borderRadius: 2,
        marginTop: "auto",
        bgcolor: (theme) => theme.palette.mode === "dark" ? "#111827" : "#FFFFFF",
        border: (theme) => theme.palette.mode === "dark" ? "1px solid #1F2937" : `1px solid ${theme.palette.divider}`,
      }}
    >
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

      <SettingsDrawer
        open={showSettingDrawer}
        onClose={() => setShowSettingDrawer(false)}
      />
    </Box>
  );
};
