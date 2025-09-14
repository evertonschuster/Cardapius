import { User } from "oidc-client-ts";
import { AuthErrorDetails } from "./AuthErrorDetails";

export interface AuthContextValue {
  user: User | null;
  isLoading: boolean;
  isAuthenticated: boolean;
  signin: () => Promise<void>;
  signinCallback: () => Promise<void>;
  signout: () => Promise<void>;
  refresh: () => Promise<void>;
  hasRole: (role: string) => boolean;
  error?: AuthErrorDetails | null;
}