import React from 'react';
import { render, screen } from '@testing-library/react';
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
    const signin = jest.fn();
    mockUseAuth.mockReturnValue({
      user: null,
      isAuthenticated: false,
      signin,
      hasRole: () => false,
      isLoading: false,
      error: null,
    });
    render(<PrivateRoute />);
    expect(signin).toHaveBeenCalled();
    expect(screen.getByText('Aguardando autenticação...')).toBeInTheDocument();
  });

  it('renders children when authenticated', () => {
    mockUseAuth.mockReturnValue({
      user: {},
      isAuthenticated: true,
      signin: jest.fn(),
      hasRole: () => true,
      isLoading: false,
      error: null,
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
      isLoading: false,
      error: null,
    });
    render(<PrivateRoute roles={['admin']} />);
    expect(screen.getByText('Acesso negado')).toBeInTheDocument();
  });

  it('renders error component when error is present', () => {
    mockUseAuth.mockReturnValue({
      user: null,
      isAuthenticated: false,
      signin: jest.fn(),
      hasRole: () => false,
      isLoading: false,
      error: { title: 'err' },
    });
    render(<PrivateRoute />);
    expect(screen.getByText('error:err')).toBeInTheDocument();
  });
});
