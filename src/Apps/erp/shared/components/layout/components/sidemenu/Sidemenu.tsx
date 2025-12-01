import { useMemo, useState } from 'react';
import { Box, Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText, OutlinedInput, Tooltip } from '@mui/material';
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import { Link as RouterLink } from 'react-router-dom';
import { useAuth } from '@shared/auth';
import { filterMenuItems, menuItems } from 'menuItems';

type SidemenuProps = {
    collapsed?: boolean;
}

export const Sidemenu: React.FC<SidemenuProps> = ({ collapsed = false }) => {
    const { hasRole } = useAuth();
    const [searchTerm, setSearchTerm] = useState('');

    const filteredItems = useMemo(
        () => filterMenuItems(menuItems, searchTerm, hasRole),
        [hasRole, searchTerm],
    );

    return (
        <>
            <Box
                sx={{
                    minHeight: 0,
                    flexGrow: 1,
                    display: 'flex',
                    flexDirection: 'column',
                }}
            >
                {!collapsed && (
                    <>
                        <OutlinedInput
                            size="small"
                            placeholder="Search"
                            startAdornment={<SearchRoundedIcon />}
                            sx={{
                                margin: 0.5,
                            }}
                            value={searchTerm}
                            onChange={(event) => setSearchTerm(event.target.value)}
                        />
                        <Divider />
                    </>
                )}

                <List
                    component="nav"
                    aria-labelledby="nested-list-subheader"
                >
                    {filteredItems.map((item) => (
                        <ListItem key={item.path} sx={{ padding: 0 }}>
                            <Tooltip title={collapsed ? item.label : ""} placement="right">
                                <ListItemButton
                                    component={RouterLink}
                                    to={item.path}
                                    sx={collapsed ? {
                                        justifyContent: "center",
                                        px: 1,
                                    } : undefined}
                                >
                                    <ListItemIcon sx={collapsed ? { minWidth: 0 } : undefined}>
                                        {item.icon}
                                    </ListItemIcon>
                                    {!collapsed && <ListItemText primary={item.label} />}
                                </ListItemButton>
                            </Tooltip>
                        </ListItem>
                    ))}
                </List>

            </Box>
        </>
    );
};
