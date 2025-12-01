import { useMemo, useState } from 'react';
import { Box, Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText, OutlinedInput } from '@mui/material'
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import { Link as RouterLink } from 'react-router-dom';
import { useAuth } from '@shared/auth';
import { MenuItem, menuItems } from 'menuItems';

export const Sidemenu: React.FC = () => {
    const { hasRole } = useAuth();
    const [searchTerm, setSearchTerm] = useState('');

    const normalize = (value: string) =>
        value
            .normalize('NFD')
            .replace(/\p{Diacritic}/gu, '')
            .toLowerCase();

    const filteredItems = useMemo(() => {
        const terms = normalize(searchTerm).split(/\s+/).filter(Boolean);

        const matchesSearch = (item: MenuItem) => {
            if (!terms.length) return true;

            const haystack = [
                normalize(item.label),
                normalize(item.path),
                ...(item.keywords?.map(normalize) ?? []),
            ].join(' ');

            return terms.every((term) => haystack.includes(term));
        };

        return menuItems.filter(
            (item) => (!item.roles || item.roles.every(hasRole)) && matchesSearch(item)
        );
    }, [hasRole, searchTerm]);

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
                    sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
                    component="nav"
                    aria-labelledby="nested-list-subheader"
                >
                    {filteredItems.map((item) => (
                        <ListItem key={item.path} sx={{ padding: 0 }} >
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
    )
}
