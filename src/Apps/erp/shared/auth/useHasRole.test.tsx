import React from 'react';
import { renderHook } from '@testing-library/react';
import { AuthContext } from './AuthProvider';
import { useHasRole } from './useHasRole';

describe('useHasRole', () => {
  it('returns true when role is present', () => {
    const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
      <AuthContext.Provider
        value={{
          user: { profile: { roles: ['admin'] } } as any,
          signin: async () => {},
          signout: async () => {},
          refresh: async () => {},
          hasRole: (r: string) => r === 'admin',
        }}
      >
        {children}
      </AuthContext.Provider>
    );
    const { result } = renderHook(() => useHasRole('admin'), { wrapper });
    expect(result.current).toBe(true);
  });

  it('returns false when role is absent', () => {
    const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
      <AuthContext.Provider
        value={{
          user: { profile: { roles: ['user'] } } as any,
          signin: async () => {},
          signout: async () => {},
          refresh: async () => {},
          hasRole: () => false,
        }}
      >
        {children}
      </AuthContext.Provider>
    );
    const { result } = renderHook(() => useHasRole('admin'), { wrapper });
    expect(result.current).toBe(false);
  });
});

