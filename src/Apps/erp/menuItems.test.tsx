import { buildSearchTerms, filterMenuItems, menuItems } from './menuItems';

describe('menuItems filtering', () => {
    const allowAllRoles = () => true;

    it('returns all accessible items when there is no search term', () => {
        const items = filterMenuItems(menuItems, '', allowAllRoles);

        expect(items).toHaveLength(menuItems.length);
    });

    it('hides items behind roles that the user does not have', () => {
        const denyPdvRole = (role: string) => role !== 'pdv';

        const items = filterMenuItems(menuItems, '', denyPdvRole);

        expect(items.find((item) => item.path === '/pdv')).toBeUndefined();
    });

    it('matches search terms against labels, paths and keywords', () => {
        const items = filterMenuItems(menuItems, 'cozinha produção', allowAllRoles);

        expect(items.map((item) => item.label)).toContain('Cozinha Inteligente');
    });

    it('ignores diacritics when filtering by keywords', () => {
        const items = filterMenuItems(menuItems, 'inventario produtos', allowAllRoles);

        expect(items.map((item) => item.path)).toContain('/estoque');
    });

    it('requires every search term to match', () => {
        const items = filterMenuItems(menuItems, 'admin inexistente', allowAllRoles);

        expect(items).toHaveLength(0);
    });
});

describe('buildSearchTerms', () => {
    it('normalizes diacritics and casing', () => {
        expect(buildSearchTerms('Início gestão')).toEqual(['inicio', 'gestao']);
    });
});
