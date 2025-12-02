import { AuthErrorDetails, SigninCallbackRespose } from "./AuthErrorDetails";
import { AuthUser } from "./AuthUser";

export interface AuthContextValue {
  user?: AuthUser | null;
  isAuthenticated: boolean;
  signin: () => Promise<AuthErrorDetails | void>;
  signinCallback: () => Promise<SigninCallbackRespose>;
  signout:  () => Promise<AuthErrorDetails | void>;
  hasRole: (role: string | string []) => boolean;
}