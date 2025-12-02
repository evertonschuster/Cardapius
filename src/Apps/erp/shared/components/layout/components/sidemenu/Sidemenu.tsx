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
                    transition: (theme) => theme.transitions.create('padding', {
                        duration: theme.transitions.duration.shorter,
                        easing: theme.transitions.easing.easeInOut,
                    }),
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
                                    sx={(theme) => ({
                                        transition: theme.transitions.create(['padding', 'justify-content'], {
                                            duration: theme.transitions.duration.shorter,
                                            easing: theme.transitions.easing.easeInOut,
                                        }),
                                        ...(collapsed ? {
                                            justifyContent: "center",
                                            px: 1,
                                        } : {}),
                                    })}
                                >
                                    <ListItemIcon
                                        sx={(theme) => ({
                                            transition: theme.transitions.create(['min-width', 'margin'], {
                                                duration: theme.transitions.duration.shorter,
                                                easing: theme.transitions.easing.easeInOut,
                                            }),
                                            ...(collapsed ? { minWidth: 0, mr: 0 } : {}),
                                        })}
                                    >
                                        {item.icon}
                                    </ListItemIcon>
                                    <ListItemText
                                        primary={item.label}
                                        sx={(theme) => ({
                                            transition: theme.transitions.create(['opacity', 'max-width'], {
                                                duration: theme.transitions.duration.shorter,
                                                easing: theme.transitions.easing.easeInOut,
                                            }),
                                            opacity: collapsed ? 0 : 1,
                                            maxWidth: collapsed ? 0 : 200,
                                            overflow: 'hidden',
                                            whiteSpace: 'nowrap',
                                        })}
                                        slotProps={{
                                            primary: { noWrap: true },
                                        }}
                                    />
                                </ListItemButton>
                            </Tooltip>
                        </ListItem>
                    ))}
                </List>

            </Box>
        </>
    );
};
