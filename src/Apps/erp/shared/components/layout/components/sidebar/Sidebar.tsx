import { Box } from "@mui/material";
import { Logo } from "@shared/components/Logo";
import { Sidemenu } from "../sidemenu/Sidemenu";
import { UserCard } from "../user-card/UserCard";

export const Sidebar: React.FC = () => {

    return (
        <Box
            sx={{
                width: 260,
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
            <Logo />

            <Sidemenu />

            <UserCard />
        </Box>

    )
};

