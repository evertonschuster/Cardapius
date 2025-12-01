import { Box, Divider, IconButton, Stack, Tooltip } from "@mui/material";
import { Logo } from "@shared/components/Logo";
import { Sidemenu } from "../sidemenu/Sidemenu";
import { UserCard } from "../user-card/UserCard";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import { useMemo, useState } from "react";

export const Sidebar: React.FC = () => {

    const [collapsed, setCollapsed] = useState(false);

    const width = useMemo(() => collapsed ? 80 : 260, [collapsed]);

    const toggleCollapsed = () => {
        setCollapsed((value) => !value);
    }

    return (
        <Box
            sx={{
                width,
                transition: (theme) =>
                    theme.transitions.create("width", {
                        duration: theme.transitions.duration.standard,
                        easing: theme.transitions.easing.easeInOut,
                    }),
                flexShrink: 0,
                bgcolor: "background.paper",
                borderRight: (theme) =>
                    `1px solid ${theme.palette.mode === "dark"
                        ? "#1F2937"
                        : theme.palette.divider
                    }`,
                display: "flex",
                flexDirection: "column",
            }}
        >
            <Stack direction="row" alignItems="center" justifyContent="space-between" px={1} py={1.5}>
                <Logo showText={!collapsed} />

                <Tooltip title={collapsed ? "Expandir" : "Recolher"}>
                    <IconButton size="small" onClick={toggleCollapsed}>
                        {collapsed ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                    </IconButton>
                </Tooltip>
            </Stack>

            <Divider />

            <Sidemenu collapsed={collapsed} />

            <UserCard collapsed={collapsed} />
        </Box>

    )
};

