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

const normalizeText = (value: string) =>
    value
        .normalize('NFD')
        .replaceAll(/\p{Diacritic}/gu, '')
        .toLowerCase();

export const buildSearchTerms = (searchTerm: string) =>
    normalizeText(searchTerm)
        .split(/\s+/)
        .filter(Boolean);

export const matchesMenuItemSearch = (item: MenuItem, terms: string[]) => {
    if (!terms.length) return true;

    const haystack = [
        normalizeText(item.label),
        normalizeText(item.path),
        ...(item.keywords?.map(normalizeText) ?? []),
    ].join(' ');

    return terms.every((term) => haystack.includes(term));
};

export const filterMenuItems = (
    items: MenuItem[],
    searchTerm: string,
    hasRole: (role: string) => boolean,
) => {
    const terms = buildSearchTerms(searchTerm);

    return items.filter(
        (item) => (!item.roles || item.roles.every(hasRole)) && matchesMenuItemSearch(item, terms),
    );
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
