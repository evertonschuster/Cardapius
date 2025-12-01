import * as React from "react";
import { Chip } from "@mui/material";

type UserCardPlanChipProps = {
  label: string;
};

export const UserCardPlanChip: React.FC<UserCardPlanChipProps> = ({
  label,
}) => {
  return (
    <Chip
      label={label}
      size="small"
      style={{
        marginRight: 12,
      }}
      sx={{
        fontSize: 11,
        bgcolor: (theme) => theme.palette.mode === "dark" ? "#DCFCE7" : "#BBF7D0",
        color: "#166534",
      }}
    />
  );
};
