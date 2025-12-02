import * as React from "react";
import { Box, Stack, Typography } from "@mui/material";
import { UserCardAvatar } from "./UserCardAvatar";

type UserCardHeaderProps = {
  name: string;
  email: string;
  avatarUrl?: string;
};

export const UserCardHeader: React.FC<UserCardHeaderProps> = ({
  name,
  email,
  avatarUrl,
}) => {
  return (
    <Stack
      direction="row"
      alignItems="center">
      <UserCardAvatar name={name} avatarUrl={avatarUrl} />

      <Box
        sx={{
          flexGrow: 1,
          minWidth: 0,
          paddingLeft: 1
        }} >
        <Typography
          variant="body1"
          noWrap
          sx={{
            fontWeight: 600,
            lineHeight: 1.2,
            overflow: "hidden",
            textOverflow: "ellipsis",
          }}>
          {name}
        </Typography>

        <Typography
          variant="caption"
          color="text.secondary"
          sx={{
            display: "block",
            mt: 0.3,
            overflow: "hidden",
            textOverflow: "ellipsis",
          }}
        >
          {email}
        </Typography>
      </Box>
    </Stack>
  );
};
