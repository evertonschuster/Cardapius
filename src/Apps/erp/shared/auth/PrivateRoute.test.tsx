import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { PrivateRoute } from './PrivateRoute';
import { useAuth } from './AuthProvider';

jest.mock('./AuthProvider', () => ({ useAuth: jest.fn() }));
jest.mock('./components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div>{title}</div>,
}));
jest.mock('./components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: any) => <div>error:{details.title}</div>,
}));

const mockUseAuth = useAuth as jest.Mock;

describe('PrivateRoute', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('calls signin and shows loader when unauthenticated', () => {
    const signin = jest.fn().mockResolvedValue(undefined);
    mockUseAuth.mockReturnValue({
      user: null,
      isAuthenticated: false,
      signin,
      hasRole: () => false,
    });

    render(<PrivateRoute />);

    expect(signin).toHaveBeenCalledTimes(1);
    expect(screen.getByText('Aguardando autenticação...')).toBeInTheDocument();
  });

  it('renders children when authenticated', () => {
    mockUseAuth.mockReturnValue({
      user: {},
      isAuthenticated: true,
      signin: jest.fn(),
      hasRole: () => true,
    });
    render(
      <PrivateRoute>
        <div>secret</div>
      </PrivateRoute>
    );
    expect(screen.getByText('secret')).toBeInTheDocument();
  });

  it('denies access when missing role', () => {
    mockUseAuth.mockReturnValue({
      user: { profile: { roles: ['user'] } },
      isAuthenticated: true,
      signin: jest.fn(),
      hasRole: (r: string) => r === 'user',
    });
    render(<PrivateRoute roles={['admin']} />);
    expect(screen.getByText('Acesso negado')).toBeInTheDocument();
  });

  it('renders error component when signin returns error', async () => {
    const signin = jest.fn().mockResolvedValue({ title: 'err' });
    mockUseAuth.mockReturnValue({
      user: null,
      isAuthenticated: false,
      signin,
      hasRole: () => false,
    });

    render(<PrivateRoute />);

    await waitFor(() => expect(screen.getByText('error:err')).toBeInTheDocument());
  });
});
