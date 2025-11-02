import { renderHook } from '@testing-library/react';
import { useHasRole } from './useHasRole';
import { useAuth } from './AuthProvider';

jest.mock('./AuthProvider', () => ({ useAuth: jest.fn() }));
const mockUseAuth = useAuth as jest.Mock;

describe('useHasRole', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('returns true when role is present', () => {
    mockUseAuth.mockReturnValue({ hasRole: (r: string) => r === 'admin' });
    const { result } = renderHook(() => useHasRole('admin'));
    expect(result.current).toBe(true);
  });

  it('returns false when role is absent', () => {
    mockUseAuth.mockReturnValue({ hasRole: () => false });
    const { result } = renderHook(() => useHasRole('admin'));
    expect(result.current).toBe(false);
  });
});
