import { Box, Divider, Input, List, ListItem, ListItemButton, ListItemIcon, ListItemText, OutlinedInput } from '@mui/material'
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';

export const Sidemenu: React.FC = () => {
    return (
        <>
            <Box
                sx={{
                    minHeight: 0,
                    overflow: 'hidden auto',
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
                        margin: 0.5
                    }}
                />
                <Divider />
                <List
                    sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
                    component="nav"
                    aria-labelledby="nested-list-subheader"
                >
                    <ListItem >
                        <ListItemButton sx={{ padding: 0 }}>
                            <ListItemIcon>
                                <HomeRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Home" />
                        </ListItemButton>
                    </ListItem>

                    <ListItem >
                        <ListItemButton sx={{ padding: 0 }}>
                            <ListItemIcon>
                                <HomeRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Home" />
                        </ListItemButton>
                    </ListItem>
                </List>
            </Box>
        </>
    )
}
