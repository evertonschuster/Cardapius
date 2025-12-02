import React from 'react';
import { render, screen } from '@testing-library/react';
import { AppThemeContext, getTheme } from './index';
import { ThemeContext, useThemeMode } from './ThemeContext';
import { usePersistentState } from '@shared/hooks/usePersistentState';

jest.mock('@shared/hooks/usePersistentState', () => ({
  usePersistentState: jest.fn(),
}));

const mockUsePersistentState = usePersistentState as jest.Mock;

describe('Theme utilities', () => {
  beforeEach(() => {
    mockUsePersistentState.mockImplementation((_key: string, initial: any) => [initial, jest.fn()]);
  });

  afterEach(() => {
    jest.clearAllMocks();
  });

  it('builds theme with selected mode', () => {
    const darkTheme = getTheme('dark');
    const lightTheme = getTheme('light');

    expect(darkTheme.palette.mode).toBe('dark');
    expect(lightTheme.palette.mode).toBe('light');
  });

  it('provides theme mode via context', () => {
    const TestComponent = () => {
      const { mode } = useThemeMode();
      return <div>mode:{mode}</div>;
    };

    render(
      <AppThemeContext>
        <TestComponent />
      </AppThemeContext>
    );

    expect(screen.getByText(/mode:dark/)).toBeInTheDocument();
  });

  it('throws when using hook without provider', () => {
    const Problem = () => {
      useThemeMode();
      return null;
    };

    expect(() => render(<Problem />)).toThrow('useThemeMode must be used inside ThemeContext provider');
  });
});
