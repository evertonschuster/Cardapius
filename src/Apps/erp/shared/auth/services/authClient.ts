import { UserManager, WebStorageStateStore } from 'oidc-client-ts';
import { AuthUser } from '../types/AuthUser';

export interface AuthClient {
  getUser(): Promise<AuthUser | null>;
  signinRedirect(args?: Record<string, unknown>): Promise<void>;
  signinRedirectCallback(): Promise<AuthUser>;
  signoutRedirect(args?: Record<string, unknown>): Promise<void>;
  events: {
    addUserLoaded(cb: (user: AuthUser) => void): void;
    addUserUnloaded(cb: () => void): void;
    addSilentRenewError(cb: (error: unknown) => void): void;
  };
  settings: {
    authority?: string;
    client_id?: string;
    redirect_uri?: string;
  };
}

export interface AuthState {
    user?: AuthUser | null;
    isAuthenticated: boolean;
}

export type UserLoadedListener = (user: AuthUser | null) => void;

export const createAuthClient = (): AuthClient => {
  const manager = new UserManager({
    client_id: import.meta.env.VITE_OIDC_CLIENT_ID || '',
    authority: import.meta.env.VITE_OIDC_AUTHORITY || '',
    redirect_uri: `${window.location.origin}/callback`,
    silent_redirect_uri: `${window.location.origin}/silent-renew`,
    post_logout_redirect_uri: `${window.location.origin}/login`,
    scope: import.meta.env.VITE_OIDC_SCOPE || 'openid profile',
    response_type: 'code',
    loadUserInfo: false,
    filterProtocolClaims: true,
    revokeTokensOnSignout: true,
    automaticSilentRenew: true,
    accessTokenExpiringNotificationTimeInSeconds: 60,
    silentRequestTimeoutInSeconds: 20,
    userStore: new WebStorageStateStore({
      store: window.localStorage,
      prefix: 'oidc',
    }),
  });

  return {
    getUser: () => manager.getUser() as Promise<AuthUser | null>,
    signinRedirect: (args) => manager.signinRedirect(args),
    signinRedirectCallback: () => manager.signinRedirectCallback() as Promise<AuthUser>,
    signoutRedirect: (args) => manager.signoutRedirect(args),
    events: {
      addUserLoaded: (cb) => manager.events.addUserLoaded(cb as any),
      addUserUnloaded: (cb) => manager.events.addUserUnloaded(cb),
      addSilentRenewError: (cb) => manager.events.addSilentRenewError(cb),
    },
    settings: manager.settings,
  };
};
