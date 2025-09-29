import { createAuthClient } from './authClient';
import { UserManager, WebStorageStateStore } from 'oidc-client-ts';

type MockedUserManager = jest.Mocked<{
  getUser: jest.Mock;
  signinRedirect: jest.Mock;
  signinRedirectCallback: jest.Mock;
  signoutRedirect: jest.Mock;
  signinSilent: jest.Mock;
  events: {
    addUserLoaded: jest.Mock;
    addUserUnloaded: jest.Mock;
    addSilentRenewError: jest.Mock;
    addAccessTokenExpiring: jest.Mock;
    removeUserLoaded: jest.Mock;
    removeUserUnloaded: jest.Mock;
    removeSilentRenewError: jest.Mock;
    removeAccessTokenExpiring: jest.Mock;
  };
  settings: {
    authority?: string;
    client_id?: string;
    redirect_uri?: string;
  };
}>;

jest.mock('oidc-client-ts', () => {
  const createManager = () => ({
    getUser: jest.fn().mockResolvedValue(null),
    signinRedirect: jest.fn().mockResolvedValue(undefined),
    signinRedirectCallback: jest.fn().mockResolvedValue({}),
    signoutRedirect: jest.fn().mockResolvedValue(undefined),
    signinSilent: jest.fn().mockResolvedValue({}),
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

  const mockCtor = jest.fn(() => createManager());
  const mockStore = jest.fn((options) => ({ options }));

  return {
    __esModule: true,
    UserManager: mockCtor,
    WebStorageStateStore: mockStore,
  };
});

describe('createAuthClient', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('configures the OIDC client with sane defaults', () => {
    const client = createAuthClient();

    expect(UserManager).toHaveBeenCalledTimes(1);
    const config = (UserManager as jest.Mock).mock.calls[0][0];

    expect(config).toMatchObject({
      client_id: '',
      authority: '',
      redirect_uri: `${window.location.origin}/callback`,
      silent_redirect_uri: `${window.location.origin}/silent-renew`,
      post_logout_redirect_uri: `${window.location.origin}/login`,
      scope: 'openid profile',
      response_type: 'code',
      loadUserInfo: false,
      filterProtocolClaims: true,
      revokeTokensOnSignout: true,
      automaticSilentRenew: true,
      accessTokenExpiringNotificationTimeInSeconds: 60,
      silentRequestTimeoutInSeconds: 20,
    });

    expect((WebStorageStateStore as jest.Mock)).toHaveBeenCalledWith({
      store: window.localStorage,
      prefix: 'oidc',
    });

    expect(client.settings).toEqual({
      authority: 'https://authority',
      client_id: 'client-id',
      redirect_uri: 'https://example.com/callback',
    });
  });

  it('exposes manager methods and events', async () => {
    const client = createAuthClient();
    const managerInstance = (UserManager as jest.Mock).mock.results[0].value as MockedUserManager;

    await client.getUser();
    expect(managerInstance.getUser).toHaveBeenCalled();

    const redirectArgs = { state: { returnTo: '/orders' } };
    await client.signinRedirect(redirectArgs);
    expect(managerInstance.signinRedirect).toHaveBeenCalledWith(redirectArgs);

    await client.signinRedirectCallback();
    expect(managerInstance.signinRedirectCallback).toHaveBeenCalled();

    await client.signoutRedirect(redirectArgs);
    expect(managerInstance.signoutRedirect).toHaveBeenCalledWith(redirectArgs);

    await client.signinSilent();
    expect(managerInstance.signinSilent).toHaveBeenCalled();

    const cb = jest.fn();
    client.events.addUserLoaded(cb);
    expect(managerInstance.events.addUserLoaded).toHaveBeenCalledWith(cb);

    client.events.removeSilentRenewError(cb);
    expect(managerInstance.events.removeSilentRenewError).toHaveBeenCalledWith(cb);
  });
});
