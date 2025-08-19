import { useAuth } from './AuthProvider';

export const useHasRole = (role: string) => {
  const { hasRole } = useAuth();
  return hasRole(role);
};

