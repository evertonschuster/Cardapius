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

  it('returns error details when signin redirect fails', async () => {
    signinRedirect.mockRejectedValueOnce(new TypeError('network down'));

    const error = await authService.signinAsync();

    expect(error?.code).toBe('network_error');
    expect(error?.description).toBe('network down');
    expect(error?.authority).toBe('auth');
  });

  it('returns redirect from signinCallbackAsync', async () => {
    const response = await authService.signinCallbackAsync();
    expect(response.redirectTo).toBe('/home');
    expect(sessionStorage.getItem('returnTo')).toBeNull();
  });

  it('returns error from signinCallbackAsync when callback fails', async () => {
    window.history.pushState({}, '', '/callback?error=server_error&error_description=bad');
    signinRedirectCallback.mockRejectedValueOnce({
      error: 'login_required',
      state: { traceId: 't1', requestId: 'r1' },
    });

    const response = await authService.signinCallbackAsync();

    expect(response.error?.title).toBe('Sua sessão expirou');
    expect(response.error?.description).toBe('bad');
    expect(response.error?.traceId).toBe('t1');
    expect(response.error?.requestId).toBe('r1');
    expect(response.error?.clientId).toBe('client');
  });

  it('sanitizes redirect when returning from auth routes', async () => {
    sessionStorage.setItem('returnTo', '/callback?next=1');
    signinRedirectCallback.mockResolvedValueOnce({ state: { returnTo: '/login/step' } });

    const response = await authService.signinCallbackAsync();

    expect(response.redirectTo).toBe('/');
    expect(sessionStorage.getItem('returnTo')).toBeNull();
  });

  it('notifies userLoaded listeners and handles listener errors', async () => {
    const okListener = jest.fn();
    const failingListener = jest.fn(() => { throw new Error('boom'); });

    const unsubscribe = authService.addUserLoaded(okListener);
    authService.addUserLoaded(failingListener);

    await authService.initAsync();
    const userLoadedHandler = addUserLoaded.mock.calls[0][0];

    userLoadedHandler({ name: 'john' } as any);
    unsubscribe();
    userLoadedHandler({ name: 'doe' } as any);

    expect(okListener).toHaveBeenCalledTimes(2);
    expect(consoleErrorSpy).toHaveBeenCalledWith(
      'Erro em listener de userLoaded:',
      expect.any(Error)
    );
  });

  it('returns error details on signout failure', async () => {
    signoutRedirect.mockRejectedValueOnce(new Error('oops'));

    const error = await authService.signoutAsync();
    expect(error?.title).toBe('Não foi possível processar sua solicitação');
    expect(error?.authority).toBe('auth');
  });
});
