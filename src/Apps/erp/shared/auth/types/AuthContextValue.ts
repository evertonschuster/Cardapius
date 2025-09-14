import { AuthErrorDetails } from "./AuthErrorDetails";
import { AuthUser } from "./AuthUser";

export interface AuthContextValue {
  user: AuthUser | null;
  isLoading: boolean;
  isAuthenticated: boolean;
  signin: () => Promise<void>;
  signinCallback: () => Promise<void>;
  signout: () => Promise<void>;
  refresh: () => Promise<void>;
  hasRole: (role: string) => boolean;
  error?: AuthErrorDetails | null;
}