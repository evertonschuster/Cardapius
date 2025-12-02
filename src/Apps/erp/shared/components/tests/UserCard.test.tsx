import React from 'react';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { UserCard } from '../layout/components/user-card/UserCard';
import { UserCardActions } from '../layout/components/user-card/UserCardActions';
import { UserCardAvatar } from '../layout/components/user-card/UserCardAvatar';
import { UserCardHeader } from '../layout/components/user-card/UserCardHeader';
import { useAuth } from '@shared/auth';

jest.mock('@shared/auth', () => ({
  useAuth: jest.fn(),
}));

const mockNavigate = jest.fn();
jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: () => mockNavigate,
}));

jest.mock('../layout/components/settings/SettingsDrawer', () => ({
  SettingsDrawer: ({ open, onClose }: { open: boolean; onClose: () => void }) => (
    <div data-testid="settings-drawer" data-open={open} onClick={onClose} />
  ),
}));

describe('UserCard', () => {
  beforeEach(() => {
    (useAuth as jest.Mock).mockReturnValue({
      user: {
        profile: {
          name: 'Jane Doe',
          email: 'jane@example.com',
        },
      },
    });
    mockNavigate.mockReset();
  });

  it('shows header information and the default plan when expanded', () => {
    render(<UserCard />);

    expect(screen.getByText('Jane Doe')).toBeInTheDocument();
    expect(screen.getByText('jane@example.com')).toBeInTheDocument();
    expect(screen.getByText('Starter')).toBeInTheDocument();
    expect(screen.getByTestId('settings-drawer')).toHaveAttribute('data-open', 'false');
  });

  it('opens the settings drawer and routes to logout when icons are pressed', async () => {
    const user = userEvent.setup();
    render(<UserCard />);

    const buttons = screen.getAllByRole('button');
    await user.click(buttons[0]);

    expect(screen.getByTestId('settings-drawer')).toHaveAttribute('data-open', 'true');

    await user.click(buttons[1]);
    expect(mockNavigate).toHaveBeenCalledWith('logout');
  });

  it('renders collapsed controls with the avatar tooltip and actions', async () => {
    const user = userEvent.setup();
    render(<UserCard collapsed />);

    const avatar = screen.getByRole('img', { name: 'Jane Doe' });
    expect(avatar).toBeInTheDocument();

    const buttons = screen.getAllByRole('button');
    await user.click(buttons[0]);
    expect(screen.getByTestId('settings-drawer')).toHaveAttribute('data-open', 'true');
  });
});

describe('UserCardActions', () => {
  beforeEach(() => {
    mockNavigate.mockReset();
  });

  it('calls the provided handlers for settings and logout', async () => {
    const onOpenSettings = jest.fn();
    const user = userEvent.setup();

    render(<UserCardActions onOpenSettings={onOpenSettings} />);

    const [settingsButton, logoutButton] = screen.getAllByRole('button');

    await user.click(settingsButton);
    expect(onOpenSettings).toHaveBeenCalledTimes(1);

    await user.click(logoutButton);
    expect(mockNavigate).toHaveBeenCalledWith('logout');
  });
});

describe('UserCardAvatar', () => {
  it('wraps the avatar with a badge when online', () => {
    const { container } = render(
      <UserCardAvatar name="Online User" avatarUrl="avatar.png" />
    );

    expect(screen.getByRole('img', { name: 'Online User' })).toBeInTheDocument();
    expect(container.querySelector('.MuiBadge-badge')).toBeInTheDocument();
  });

  it('skips the badge when offline', () => {
    const { container } = render(
      <UserCardAvatar name="Offline User" avatarUrl="avatar.png" online={false} />
    );

    expect(screen.getByRole('img', { name: 'Offline User' })).toBeInTheDocument();
    expect(container.querySelector('.MuiBadge-badge')).not.toBeInTheDocument();
  });
});

describe('UserCardHeader', () => {
  it('shows user identity and the associated plan chip', () => {
    render(
      <UserCardHeader
        name="Header User"
        email="header@example.com"
        avatarUrl="header.png"
      />
    );

    expect(screen.getByText('Header User')).toBeInTheDocument();
    expect(screen.getByText('header@example.com')).toBeInTheDocument();
    expect(screen.getByText('Premium')).toBeInTheDocument();
  });
});
