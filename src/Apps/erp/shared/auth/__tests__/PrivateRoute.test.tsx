import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { PrivateRoute } from '../PrivateRoute';
import { useAuth } from '../AuthProvider';
import { AuthErrorDetails, SigninCallbackRespose } from '../types/AuthErrorDetails';

jest.mock('../AuthProvider');
jest.mock('../components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div data-testid="progress">{title}</div>
}));
jest.mock('../components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: { details: AuthErrorDetails }) => (
    <div data-testid="error">{details.title ?? 'Erro ao autenticar'}</div>
  )
}));

type UseAuthReturn = {
  signin: jest.Mock<Promise<AuthErrorDetails | void>, []>;
  signinCallback: jest.Mock<Promise<SigninCallbackRespose>, []>;
  signout: jest.Mock<Promise<AuthErrorDetails | void>, []>;
  hasRole: jest.Mock<boolean, [string | string[]]>;
  isAuthenticated: boolean;
};

const mockUseAuth = useAuth as jest.MockedFunction<typeof useAuth>;

const createAuth = (overrides?: Partial<UseAuthReturn>): UseAuthReturn => ({
  signin: jest.fn(async () => undefined),
  signinCallback: jest.fn(async () => ({ redirectTo: null, error: null })),
  signout: jest.fn(async () => undefined),
  hasRole: jest.fn((_role: string | string[]) => true),
  isAuthenticated: true,
  ...overrides
});

describe('PrivateRoute', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('renders children when authenticated and authorized', () => {
    mockUseAuth.mockReturnValue(createAuth());

    render(
      <PrivateRoute roles={['admin']}>
        <div>Secret</div>
      </PrivateRoute>
    );

    expect(screen.getByText('Secret')).toBeInTheDocument();
  });

  it('shows access denied when the user lacks required roles', () => {
    const auth = createAuth({ hasRole: jest.fn((_role: string | string[]) => false) });
    mockUseAuth.mockReturnValue(auth);

    render(
      <PrivateRoute roles={['admin']}>
        <div>Secret</div>
      </PrivateRoute>
    );

    expect(screen.getByText('Acesso negado')).toBeInTheDocument();
    expect(auth.signin).not.toHaveBeenCalled();
  });

  it('starts sign-in once when unauthenticated', async () => {
    const signin = jest.fn(async () => undefined);
    mockUseAuth.mockReturnValue(createAuth({ isAuthenticated: false, signin }));

    render(<PrivateRoute />);

    expect(screen.getByTestId('progress')).toHaveTextContent('Aguardando autenticação...');
    await waitFor(() => expect(signin).toHaveBeenCalledTimes(1));
  });

  it('surfaces sign-in errors through the error details component', async () => {
    const signinError: AuthErrorDetails = { title: 'Falha', description: 'Erro ao autenticar' };
    const signin = jest.fn(async () => signinError);
    mockUseAuth.mockReturnValue(createAuth({ isAuthenticated: false, signin }));

    render(<PrivateRoute />);

    await waitFor(() => expect(screen.getByTestId('error')).toHaveTextContent('Falha'));
  });
});
