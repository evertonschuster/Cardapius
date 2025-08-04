interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  roles: string[];
}

import { API_BASE_URL } from '@shared/config';

const mockRoles = ['admin', 'pdv'];

export const authService = {
  async login(username: string, _password: string): Promise<LoginResponse> {
    const url = `${API_BASE_URL}/auth/login`;
    // TODO: replace with real API call using the URL above
    return {
      accessToken: `access-${username}-${Date.now()}`,
      refreshToken: `refresh-${username}-${Date.now()}`,
      roles: mockRoles,
    };
  },
  async refreshToken(token: string): Promise<LoginResponse> {
    // simulate role update when refreshing
    const newRoles = token.includes('admin') ? ['admin', 'estoque'] : mockRoles;
    return {
      accessToken: `access-${Date.now()}`,
      refreshToken: `refresh-${Date.now()}`,
      roles: newRoles,
    };
  },
  async loadSession(_accessToken: string): Promise<{ roles: string[] }> {
    // mock loading of session data
    return { roles: mockRoles };
  },
};
