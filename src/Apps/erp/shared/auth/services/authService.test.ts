const addUserLoaded = jest.fn();
const addUserUnloaded = jest.fn();
const addSilentRenewError = jest.fn();
const getUser = jest.fn();
const signinRedirect = jest.fn();
const signinRedirectCallback = jest.fn();
const signoutRedirect = jest.fn();

const mockUserManager = {
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
  getUser,
  signinRedirect,
  signinRedirectCallback,
  signoutRedirect,
};

jest.mock('./authClient', () => ({
  createAuthClient: jest.fn(() => mockUserManager),
}));

import authService from './authService';
import { createAuthClient } from './authClient';

const mockCreateAuthClient = createAuthClient as jest.Mock;

describe('authService', () => {
  let consoleErrorSpy: jest.SpyInstance;

  beforeEach(() => {
    jest.clearAllMocks();
    mockCreateAuthClient.mockClear();
    consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
    getUser.mockResolvedValue({ expired: false });
    signinRedirect.mockResolvedValue(undefined);
    signinRedirectCallback.mockResolvedValue({ state: { returnTo: '/home' } });
    signoutRedirect.mockResolvedValue(undefined);
    sessionStorage.clear();
    window.history.pushState({}, '', '/');
  });

  afterEach(() => {
    consoleErrorSpy.mockRestore();
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
    expect(consoleErrorSpy).toHaveBeenCalledWith(
      'Error loading user session:',
      expect.any(Error)
    );
  });

  it('performs signin redirect and stores returnTo', async () => {
    window.history.pushState({}, '', '/test?a=1');

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
