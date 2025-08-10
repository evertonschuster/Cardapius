import { User } from '@shared/types';
import { API_BASE_URL } from '@shared/config';

export const fetchUsers = async (): Promise<User[]> => {
  const url = `${API_BASE_URL}/users`;
  // TODO: fetch users from API using the URL above
  return [];
};
