import { ReactNode } from 'react';
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import PointOfSaleIcon from '@mui/icons-material/PointOfSale';
import KitchenIcon from '@mui/icons-material/Kitchen';
import InventoryIcon from '@mui/icons-material/Inventory';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';


export type MenuItem = {
    label: string;
    path: string;
    icon: ReactNode;
    roles?: string[];
    keywords?: string[];
};

export const menuItems: MenuItem[] = [
    {
        label: 'Home',
        path: '/',
        icon: <HomeRoundedIcon />,
        keywords: ['dashboard', 'inicio'],
    },
    {
        label: 'Admin',
        path: '/admin',
        icon: <AdminPanelSettingsIcon />,
        keywords: ['configurações', 'usuarios', 'perfis'],
    },
    {
        label: 'PDV',
        path: '/pdv',
        icon: <PointOfSaleIcon />,
        roles: ['pdv'],
        keywords: ['ponto de venda', 'caixa'],
    },
    {
        label: 'Cozinha Inteligente',
        path: '/smart-kitchen',
        icon: <KitchenIcon />,
        roles: ['smart-kitchen'],
        keywords: ['pedidos', 'produção', 'cozinha'],
    },
    {
        label: 'Estoque',
        path: '/estoque',
        icon: <InventoryIcon />,
        roles: ['estoque'],
        keywords: ['inventário', 'produtos'],
    },
];