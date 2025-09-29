import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import { act, renderHook, waitFor } from '@testing-library/react';
import { AuthProvider, useAuth } from './AuthProvider';
import { AuthClient } from './services/authClient';
import { OidcService } from './services/oidcService';

const mockNavigate = jest.fn();

jest.mock('react-router-dom', () => {
  const actual = jest.requireActual('react-router-dom');
  return {
    ...actual,
    useNavigate: () => mockNavigate,
  };
});

jest.mock('./services/oidcService', () => ({
  OidcService: {
    getOidcParamsFromUrl: jest.fn().mockReturnValue({}),
  },
}));

type MockClient = jest.Mocked<AuthClient>;

const createMockClient = (): MockClient => ({
  getUser: jest.fn().mockResolvedValue(null),
  signinRedirect: jest.fn().mockResolvedValue(undefined),
  signinRedirectCallback: jest.fn().mockResolvedValue({ state: {} } as any),
  signoutRedirect: jest.fn().mockResolvedValue(undefined),
  signinSilent: jest.fn().mockResolvedValue({} as any),
  events: {
    addUserLoaded: jest.fn(),
    addUserUnloaded: jest.fn(),
    addSilentRenewError: jest.fn(),
    addAccessTokenExpiring: jest.fn(),
    removeUserLoaded: jest.fn(),
    removeUserUnloaded: jest.fn(),
    removeSilentRenewError: jest.fn(),
    removeAccessTokenExpiring: jest.fn(),
  },
  settings: {
    authority: 'https://authority',
    client_id: 'client-id',
    redirect_uri: 'https://example.com/callback',
  },
});

const renderAuthHook = (client: MockClient, initialEntry = '/dashboard') =>
  renderHook(() => useAuth(), {
    wrapper: ({ children }) => (
      <MemoryRouter initialEntries={[initialEntry]}>
        <AuthProvider client={client}>{children}</AuthProvider>
      </MemoryRouter>
    ),
  });

describe('AuthProvider', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    sessionStorage.clear();
  });

  it('throws when useAuth is used outside the provider', () => {
    expect(() => renderHook(() => useAuth())).toThrow('useAuth must be used within AuthProvider');
  });

  it('signs in and stores return url', async () => {
    const client = createMockClient();
    const { result } = renderAuthHook(client, '/orders?status=open#row');

    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signin();
    });

    expect(sessionStorage.getItem('returnTo')).toBe('/orders?status=open#row');
    expect(client.signinRedirect).toHaveBeenCalledWith({ state: { returnTo: '/orders?status=open#row' } });
  });

  it('builds error details when signin fails', async () => {
    const client = createMockClient();
    client.signinRedirect.mockRejectedValueOnce({
      error: 'login_required',
      error_description: 'Need login',
      error_uri: 'https://errors/1',
      state: { traceId: 'trace-1', requestId: 'req-1' },
    });
    (OidcService.getOidcParamsFromUrl as jest.Mock).mockReturnValueOnce({});

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signin();
    });

    await waitFor(() => expect(result.current.error).not.toBeNull());

    expect(result.current.error).toEqual(
      expect.objectContaining({
        title: 'Sua sessão expirou',
        description: 'Need login',
        code: 'login_required',
        errorUri: 'https://errors/1',
        traceId: 'trace-1',
        requestId: 'req-1',
        authority: 'https://authority',
        clientId: 'client-id',
        redirectUri: 'https://example.com/callback',
      })
    );
  });

  it('handles signin callback and navigation', async () => {
    const client = createMockClient();
    const user = { expired: false, state: { returnTo: '/welcome' } } as any;
    client.signinRedirectCallback.mockResolvedValueOnce(user);

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signinCallback();
    });

    expect(client.signinRedirectCallback).toHaveBeenCalled();
    expect(mockNavigate).toHaveBeenCalledWith('/welcome', { replace: true });
    expect(result.current.user).toBe(user);
    expect(sessionStorage.getItem('returnTo')).toBeNull();
  });

  it('redirects to home when callback returnTo is an auth route', async () => {
    const client = createMockClient();
    const user = { expired: false, state: { returnTo: '/login' } } as any;
    client.signinRedirectCallback.mockResolvedValueOnce(user);

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signinCallback();
    });

    expect(mockNavigate).toHaveBeenCalledWith('/', { replace: true });
  });

  it('captures callback errors', async () => {
    const client = createMockClient();
    client.signinRedirectCallback.mockRejectedValueOnce({
      error: 'server_error',
      error_description: 'Callback failed',
      state: { traceId: 'trace-cb' },
    });
    (OidcService.getOidcParamsFromUrl as jest.Mock).mockReturnValueOnce({});

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signinCallback();
    });

    await waitFor(() => expect(result.current.error).not.toBeNull());
    expect(result.current.error).toEqual(
      expect.objectContaining({
        code: 'server_error',
        description: 'Callback failed',
        traceId: 'trace-cb',
      })
    );
  });

  it('signs out and resets user', async () => {
    const client = createMockClient();
    client.getUser.mockResolvedValueOnce({ expired: false } as any);

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signout();
    });

    expect(client.signoutRedirect).toHaveBeenCalledWith({ state: { returnTo: '/' } });
    expect(result.current.user).toBeNull();
  });

  it('captures signout errors', async () => {
    const client = createMockClient();
    client.signoutRedirect.mockRejectedValueOnce({
      error: 'logout_failed',
      error_description: 'Could not logout',
    });
    (OidcService.getOidcParamsFromUrl as jest.Mock).mockReturnValueOnce({});

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.signout();
    });

    await waitFor(() => expect(result.current.error).not.toBeNull());
    expect(result.current.error).toEqual(
      expect.objectContaining({
        code: 'logout_failed',
        description: 'Could not logout',
      })
    );
    expect(result.current.user).toBeNull();
  });

  it('refreshes token and captures errors', async () => {
    const client = createMockClient();
    client.signinSilent.mockRejectedValueOnce(new TypeError('Network'));
    (OidcService.getOidcParamsFromUrl as jest.Mock).mockReturnValueOnce({ error_uri: 'https://errors/refresh' });

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    await act(async () => {
      await result.current.refresh();
    });

    await waitFor(() => expect(result.current.error).not.toBeNull());
    expect(result.current.error).toEqual(
      expect.objectContaining({
        code: 'network_error',
        errorUri: 'https://errors/refresh',
      })
    );
  });

  it('subscribes to client events and cleans up on unmount', async () => {
    const client = createMockClient();
    const { result, unmount } = renderAuthHook(client);

    await waitFor(() => expect(result.current.isLoading).toBe(false));

    expect(client.events.addUserLoaded).toHaveBeenCalledTimes(1);
    const addUserLoaded = client.events.addUserLoaded as jest.Mock;
    const addUserUnloaded = client.events.addUserUnloaded as jest.Mock;
    const addSilentRenewError = client.events.addSilentRenewError as jest.Mock;
    const onUserLoaded = addUserLoaded.mock.calls[0][0] as (user: any) => void;
    const onUserUnloaded = addUserUnloaded.mock.calls[0][0] as () => void;
    const onSilentRenewError = addSilentRenewError.mock.calls[0][0] as () => void;

    const user = { expired: false } as any;
    act(() => {
      onUserLoaded(user);
    });
    expect(result.current.user).toBe(user);

    act(() => {
      onUserUnloaded();
    });
    expect(result.current.user).toBeNull();

    act(() => {
      onSilentRenewError();
    });
    expect(result.current.isLoading).toBe(false);

    unmount();

    expect(client.events.removeUserLoaded).toHaveBeenCalledWith(onUserLoaded);
    expect(client.events.removeUserUnloaded).toHaveBeenCalledWith(onUserUnloaded);
    expect(client.events.removeSilentRenewError).toHaveBeenCalled();
  });

  it('computes role membership via hasRole helper', async () => {
    const client = createMockClient();
    client.getUser.mockResolvedValueOnce({ expired: false, profile: { roles: ['admin'] } } as any);

    const { result } = renderAuthHook(client);
    await waitFor(() => expect(result.current.isLoading).toBe(false));

    expect(result.current.hasRole('admin')).toBe(true);
    expect(result.current.hasRole('user')).toBe(false);
  });
});
