import authService from './authService';
import { createAuthClient } from './authClient';

jest.mock('./authClient');

const addUserLoaded = jest.fn();
const addUserUnloaded = jest.fn();
const addSilentRenewError = jest.fn();
const getUser = jest.fn();
const signinRedirect = jest.fn();
const signinRedirectCallback = jest.fn();
const signoutRedirect = jest.fn();

const mockCreateAuthClient = createAuthClient as jest.Mock;

describe('authService', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    mockCreateAuthClient.mockReturnValue({
      getUser,
      signinRedirect,
      signinRedirectCallback,
      signoutRedirect,
      events: {
        addUserLoaded,
        addUserUnloaded,
        addSilentRenewError,
      },
      settings: {
        authority: 'auth',
        client_id: 'client',
        redirect_uri: '/callback',
      },
    });
    getUser.mockResolvedValue({ expired: false });
    signinRedirect.mockResolvedValue(undefined);
    signinRedirectCallback.mockResolvedValue({ state: { returnTo: '/home' } });
    signoutRedirect.mockResolvedValue(undefined);
    sessionStorage.clear();
  });

  it('initializes only once and returns auth state', async () => {
    const first = await authService.initAsync();
    const second = await authService.initAsync();

    expect(addUserLoaded).toHaveBeenCalledTimes(1);
    expect(addUserUnloaded).toHaveBeenCalledTimes(1);
    expect(addSilentRenewError).toHaveBeenCalledTimes(1);
    expect(first.isAuthenticated).toBe(true);
    expect(second.isAuthenticated).toBe(true);
  });

  it('handles getUser failure gracefully', async () => {
    getUser.mockRejectedValueOnce(new Error('fail'));

    const state = await authService.initAsync();
    expect(state.isAuthenticated).toBe(false);
  });

  it('performs signin redirect and stores returnTo', async () => {
    Object.defineProperty(window, 'location', {
      value: { pathname: '/test', search: '?a=1' },
      writable: true,
    });

    await authService.signinAsync();
    expect(sessionStorage.getItem('returnTo')).toBe('/test?a=1');
    expect(signinRedirect).toHaveBeenCalled();
  });

  it('returns redirect from signinCallbackAsync', async () => {
    const response = await authService.signinCallbackAsync();
    expect(response.redirectTo).toBe('/home');
    expect(sessionStorage.getItem('returnTo')).toBeNull();
  });

  it('returns error details on signout failure', async () => {
    signoutRedirect.mockRejectedValueOnce(new Error('oops'));

    const error = await authService.signoutAsync();
    expect(error?.title).toBe('Não foi possível processar sua solicitação');
    expect(error?.authority).toBe('auth');
  });
});
