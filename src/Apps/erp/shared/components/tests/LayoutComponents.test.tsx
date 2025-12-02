import React from 'react';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MemoryRouter, Route, Routes } from 'react-router-dom';

import { AppLayout } from '../layout/AppLayout';
import { Layout } from '../../ui/Layout';
import { Sidebar } from '../layout/components/sidebar/Sidebar';
import { Sidemenu } from '../layout/components/sidemenu/Sidemenu';

jest.mock('@shared/components/Logo', () => ({
  Logo: ({ showText }: { showText?: boolean }) => (
    <div data-testid="logo" data-show-text={showText} />
  ),
}));

jest.mock('../layout/components/user-card/UserCard', () => ({
  UserCard: ({ collapsed }: { collapsed?: boolean }) => (
    <div data-testid="user-card" data-collapsed={collapsed} />
  ),
}));

const setCollapsedMock = jest.fn();
jest.mock('@shared/hooks/usePersistentState', () => ({
  usePersistentState: jest.fn(),
}));

jest.mock('@shared/auth', () => ({
  useAuth: jest.fn(() => ({ hasRole: hasRoleMock })),
}));

const hasRoleMock = jest.fn((role?: string) => Boolean(role));

describe('AppLayout', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    const { usePersistentState } = jest.requireMock('@shared/hooks/usePersistentState');
    (usePersistentState as jest.Mock).mockReturnValue([false, setCollapsedMock]);
  });

  it('renders the sidebar and provided children when present', () => {
    render(
      <MemoryRouter>
        <AppLayout>
          <div>Child Content</div>
        </AppLayout>
      </MemoryRouter>
    );

    expect(screen.getByPlaceholderText('Search')).toBeInTheDocument();
    expect(screen.getByText('Child Content')).toBeInTheDocument();
  });

  it('falls back to rendering the outlet content', () => {
    render(
      <MemoryRouter initialEntries={["/"]}>
        <Routes>
          <Route path="/" element={<AppLayout />}>
            <Route index element={<div>Outlet Value</div>} />
          </Route>
        </Routes>
      </MemoryRouter>
    );

    expect(screen.getByText('Outlet Value')).toBeInTheDocument();
  });
});

jest.mock('@shared/theme', () => ({
  AppThemeContext: ({ children }: { children: React.ReactNode }) => (
    <div data-testid="theme-wrapper">{children}</div>
  ),
}));

describe('Layout', () => {
  it('wraps children with the theme provider container', () => {
    render(
      <Layout>
        <div>Layout Child</div>
      </Layout>
    );

    expect(screen.getByTestId('theme-wrapper')).toBeInTheDocument();
    expect(screen.getByText('Layout Child')).toBeInTheDocument();
  });
});

describe('Sidebar', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('shows expanded navigation with visible labels', async () => {
    const user = userEvent.setup();
    const { usePersistentState } = jest.requireMock('@shared/hooks/usePersistentState');
    (usePersistentState as jest.Mock).mockReturnValue([false, setCollapsedMock]);

    render(
      <MemoryRouter>
        <Sidebar />
      </MemoryRouter>
    );

    expect(screen.getByTestId('logo')).toHaveAttribute('data-show-text', 'true');
    expect(screen.getByPlaceholderText('Search')).toBeInTheDocument();
    expect(screen.getByTestId('user-card')).toHaveAttribute('data-collapsed', 'false');

    await user.click(screen.getByRole('button'));
    expect(setCollapsedMock).toHaveBeenCalledTimes(1);
    const updater = setCollapsedMock.mock.calls[0][0];
    expect(typeof updater).toBe('function');
    expect(updater(false)).toBe(true);
  });

  it('collapses the sidebar and passes the collapsed flag forward', () => {
    const { usePersistentState } = jest.requireMock('@shared/hooks/usePersistentState');
    (usePersistentState as jest.Mock).mockReturnValue([true, setCollapsedMock]);

    render(
      <MemoryRouter>
        <Sidebar />
      </MemoryRouter>
    );

    expect(screen.getByTestId('logo')).toHaveAttribute('data-show-text', 'false');
    expect(screen.queryByPlaceholderText('Search')).not.toBeInTheDocument();
    expect(screen.getByTestId('user-card')).toHaveAttribute('data-collapsed', 'true');
  });
});

describe('Sidemenu', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    hasRoleMock.mockReturnValue(true);
  });

  it('filters menu items by the search term', async () => {
    const user = userEvent.setup();

    render(
      <MemoryRouter>
        <Sidemenu />
      </MemoryRouter>
    );

    await user.type(screen.getByPlaceholderText('Search'), 'admin');

    const links = screen.getAllByRole('link');
    expect(links).toHaveLength(1);
    expect(screen.getByText('Admin')).toBeInTheDocument();
  });

  it('hides the search input when collapsed', () => {
    render(
      <MemoryRouter>
        <Sidemenu collapsed />
      </MemoryRouter>
    );

    expect(screen.queryByPlaceholderText('Search')).not.toBeInTheDocument();
    expect(screen.getAllByRole('link').length).toBeGreaterThan(0);
  });

  it('applies role-based filtering to the available routes', () => {
    hasRoleMock.mockImplementation((role?: string) => role === 'pdv');

    render(
      <MemoryRouter>
        <Sidemenu />
      </MemoryRouter>
    );

    const labels = screen.getAllByRole('link').map((link) => link.textContent);
    expect(labels).toEqual(expect.arrayContaining(['Home', 'Admin', 'PDV']));
    expect(labels).not.toEqual(expect.arrayContaining(['Cozinha Inteligente', 'Estoque']));
  });
});
