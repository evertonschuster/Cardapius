import React, { useState } from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MemoryRouter, useNavigate } from 'react-router-dom';
import { ThemeContext, ThemeMode } from '@shared/theme/ThemeContext';
import { AppLayout } from '../AppLayout';
import { Sidebar } from '../components/sidebar/Sidebar';
import { Sidemenu } from '../components/sidemenu/Sidemenu';
import { SettingsDrawer } from '../components/settings/SettingsDrawer';
import { SettingsHeader } from '../components/settings/SettingsHeader';
import { SettingsSection } from '../components/settings/SettingsSection';
import { ModeSection } from '../components/settings/ModeSection';
import { Logo } from '../../Logo';
import { Layout } from '@shared/ui/Layout';
import { UserCard } from '../components/user-card/UserCard';
import { UserCardActions } from '../components/user-card/UserCardActions';
import { UserCardAvatar } from '../components/user-card/UserCardAvatar';
import { UserCardHeader } from '../components/user-card/UserCardHeader';
import { UserCardPlanChip } from '../components/user-card/UserCardPlanChip';
import { useAuth } from '@shared/auth';
import { usePersistentState } from '@shared/hooks/usePersistentState';

jest.mock('@shared/auth', () => ({
  useAuth: jest.fn(),
}));

jest.mock('@shared/hooks/usePersistentState', () => ({
  usePersistentState: jest.fn(),
}));

jest.mock('react-router-dom', () => {
  const actual = jest.requireActual('react-router-dom');
  return {
    ...actual,
    useNavigate: jest.fn(),
  };
});

jest.mock('menuItems', () => ({
  menuItems: [
    { path: '/home', label: 'Home', icon: <span>H</span> },
    { path: '/about', label: 'About', icon: <span>A</span> },
  ],
  filterMenuItems: (items: any[], term: string) =>
    items.filter((i) => i.label.toLowerCase().includes(term.toLowerCase())),
}));

const mockUseAuth = useAuth as jest.Mock;
const mockUsePersistentState = usePersistentState as jest.Mock;
const mockUseNavigate = useNavigate as jest.Mock;

const renderWithTheme = (ui: React.ReactNode, mode: ThemeMode = 'dark') =>
  render(
    <ThemeContext.Provider value={{ mode, setMode: jest.fn() }}>
      {ui}
    </ThemeContext.Provider>
  );

describe('Layout components', () => {
  beforeEach(() => {
    mockUseAuth.mockReturnValue({
      user: { profile: { name: 'Jane Doe', email: 'jane@example.com' } },
      hasRole: () => true,
    });
    mockUsePersistentState.mockImplementation((_key: string, initial: any) =>
      useState(initial)
    );
    mockUseNavigate.mockReturnValue(jest.fn());
  });

  afterEach(() => {
    jest.clearAllMocks();
  });

  it('renders AppLayout with children instead of outlet', () => {
    render(
      <ThemeContext.Provider value={{ mode: 'light', setMode: jest.fn() }}>
        <MemoryRouter>
          <AppLayout>
            <div data-testid="content">Child</div>
          </AppLayout>
        </MemoryRouter>
      </ThemeContext.Provider>
    );

    expect(screen.getByText('Cardapius')).toBeInTheDocument();
    expect(screen.getByTestId('content')).toHaveTextContent('Child');
  });

  it('renders Layout wrapper', () => {
    render(
      <Layout>
        <div data-testid="inner">Layout child</div>
      </Layout>
    );

    expect(screen.getByTestId('inner')).toHaveTextContent('Layout child');
  });

  it('renders Logo text conditionally', () => {
    const { rerender } = render(<Logo />);
    expect(screen.getByText('Cardapius')).toBeInTheDocument();

    rerender(<Logo showText={false} />);
    expect(screen.queryByText('Cardapius')).not.toBeInTheDocument();
  });

  it('renders SettingsHeader and triggers onClose', () => {
    const onClose = jest.fn();
    render(<SettingsHeader onClose={onClose} />);
    fireEvent.click(screen.getByLabelText(/close settings/i));
    expect(onClose).toHaveBeenCalled();
  });

  it('renders SettingsSection label and children', () => {
    render(
      <SettingsSection label="Test label">
        <div>inside</div>
      </SettingsSection>
    );

    expect(screen.getByText('Test label')).toBeInTheDocument();
    expect(screen.getByText('inside')).toBeInTheDocument();
  });

  it('renders ModeSection and updates theme mode', async () => {
    const setMode = jest.fn();
    render(
      <ThemeContext.Provider value={{ mode: 'light', setMode }}>
        <ModeSection />
      </ThemeContext.Provider>
    );

    await userEvent.click(screen.getByRole('button', { name: /dark/i }));
    expect(setMode).toHaveBeenCalledWith('dark');
  });

  it('renders SettingsDrawer content when open', () => {
    render(
      <ThemeContext.Provider value={{ mode: 'light', setMode: jest.fn() }}>
        <SettingsDrawer open={true} onClose={jest.fn()} />
      </ThemeContext.Provider>
    );

    expect(screen.getByText(/Configurações do sistema/i)).toBeInTheDocument();
  });

  it('renders Sidemenu and filters by search term', async () => {
    render(
      <MemoryRouter>
        <Sidemenu />
      </MemoryRouter>
    );

    expect(screen.getByPlaceholderText(/search/i)).toBeInTheDocument();
    expect(screen.getByText('Home')).toBeInTheDocument();

    await userEvent.type(screen.getByPlaceholderText(/search/i), 'abo');
    expect(screen.queryByText('Home')).not.toBeInTheDocument();
    expect(screen.getByText('About')).toBeInTheDocument();
  });

  it('renders Sidebar and toggles collapsed state', () => {
    render(
      <ThemeContext.Provider value={{ mode: 'light', setMode: jest.fn() }}>
        <MemoryRouter>
          <Sidebar />
        </MemoryRouter>
      </ThemeContext.Provider>
    );

    expect(screen.getByText(/Cardapius/)).toBeInTheDocument();
    const toggle = screen.getByLabelText(/recolher/i);
    fireEvent.click(toggle);
    expect(screen.getByLabelText(/recolher/i)).toBeInTheDocument();
  });

  it('renders UserCard in expanded mode', () => {
    const navigate = jest.fn();
    mockUseNavigate.mockReturnValue(navigate);

    render(
      <ThemeContext.Provider value={{ mode: 'dark', setMode: jest.fn() }}>
        <MemoryRouter>
          <UserCard collapsed={false} />
        </MemoryRouter>
      </ThemeContext.Provider>
    );

    expect(screen.getByText('Jane Doe')).toBeInTheDocument();
    expect(screen.getByText('jane@example.com')).toBeInTheDocument();
    expect(screen.getByText('Starter')).toBeInTheDocument();
  });

  it('renders UserCardActions and navigates on signout', () => {
    const navigate = jest.fn();
    mockUseNavigate.mockReturnValue(navigate);

    render(
      <MemoryRouter>
        <UserCardActions onOpenSettings={jest.fn()} />
      </MemoryRouter>
    );

    fireEvent.click(screen.getByRole('button', { name: /logout/i }));
    expect(navigate).toHaveBeenCalledWith('logout');
  });

  it('renders UserCardAvatar online and offline', () => {
    renderWithTheme(<UserCardAvatar name="John" avatarUrl="/a.png" online={true} />);
    expect(screen.getByAltText('John')).toBeInTheDocument();

    renderWithTheme(<UserCardAvatar name="John" avatarUrl="/a.png" online={false} />);
    expect(screen.getAllByAltText('John').length).toBeGreaterThan(1);
  });

  it('renders UserCardHeader with plan chip', () => {
    renderWithTheme(
      <UserCardHeader
        name="Header User"
        email="header@example.com"
        planLabel="Pro"
      />
    );

    expect(screen.getByText('Header User')).toBeInTheDocument();
    expect(screen.getByText('header@example.com')).toBeInTheDocument();
    expect(screen.getByText('Pro')).toBeInTheDocument();
  });

  it('renders UserCardPlanChip label', () => {
    renderWithTheme(<UserCardPlanChip label="Gold" />);
    expect(screen.getByText('Gold')).toBeInTheDocument();
  });
});
