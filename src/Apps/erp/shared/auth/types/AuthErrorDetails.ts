export interface AuthErrorDetails {
  title?: string | null;
  description?: string | null;

  code?: string | null;
  errorUri?: string | null;
  traceId?: string | null;
  requestId?: string | null;
  timestamp?: string | null;
  authority?: string | null;
  clientId?: string | null;
  redirectUri?: string | null;
}