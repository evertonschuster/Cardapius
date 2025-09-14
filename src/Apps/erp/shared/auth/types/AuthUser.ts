export interface AuthUser {
  profile?: Record<string, unknown>;
  [key: string]: unknown;
}
