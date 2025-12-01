import * as React from "react";
import { Avatar, Badge } from "@mui/material";

type UserCardAvatarProps = {
  name: string;
  avatarUrl?: string;
  online?: boolean;
};

export const UserCardAvatar: React.FC<UserCardAvatarProps> = ({
  name,
  avatarUrl,
  online = true,
}) => {
  if (!online) {
    return <Avatar src={avatarUrl} alt={name} sx={{ width: 40, height: 40 }} />;
  }

  return (
    <Badge
      overlap="circular"
      anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
      variant="dot"
      sx={{
        "& .MuiBadge-badge": (theme) => ({
          backgroundColor: "#22C55E",
          border: `2px solid ${theme.palette.mode === "dark" ? "#111827" : theme.palette.background.paper}`,
          width: 10,
          height: 10,
        }),
      }}
    >
      <Avatar src={avatarUrl} alt={name} sx={{ width: 40, height: 40 }} />
    </Badge>
  );
};
