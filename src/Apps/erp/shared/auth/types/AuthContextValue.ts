import { AuthErrorDetails } from "./AuthErrorDetails";
import { AuthUser } from "./AuthUser";

export interface AuthContextValue {
  user?: AuthUser | null;
  isAuthenticated: boolean;
  signin: () => Promise<AuthErrorDetails | void>;
  signinCallback: () => Promise<AuthErrorDetails | void>;
  signout:  () => Promise<AuthErrorDetails | void>;
  hasRole: (role: string | string []) => boolean;
}