import { useMemo, useState } from 'react';
import { Box, Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText, OutlinedInput } from '@mui/material';
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import { Link as RouterLink } from 'react-router-dom';
import { useAuth } from '@shared/auth';
import { filterMenuItems, menuItems } from 'menuItems';

export const Sidemenu: React.FC = () => {
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

                <List
                    component="nav"
                    aria-labelledby="nested-list-subheader"
                >
                    {filteredItems.map((item) => (
                        <ListItem key={item.path} sx={{ padding: 0 }}>
                            <ListItemButton component={RouterLink} to={item.path}>
                                <ListItemIcon>
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.label} />
                            </ListItemButton>
                        </ListItem>
                    ))}
                </List>

            </Box>
        </>
    );
};
