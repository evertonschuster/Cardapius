import React from 'react';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ModeSection } from '../layout/components/settings/ModeSection';
import { SettingsDrawer } from '../layout/components/settings/SettingsDrawer';
import { SettingsHeader } from '../layout/components/settings/SettingsHeader';
import { SettingsSection } from '../layout/components/settings/SettingsSection';
import { ThemeContext, ThemeMode } from '../../theme/ThemeContext';

const renderWithTheme = (
  ui: React.ReactNode,
  { mode = 'light', setMode = jest.fn() }: { mode?: ThemeMode; setMode?: jest.Mock }
) => {
  return render(
    <ThemeContext.Provider value={{ mode, setMode }}>
      {ui}
    </ThemeContext.Provider>
  );
};

describe('SettingsSection', () => {
  it('shows the provided label and content', () => {
    render(
      <SettingsSection label="Appearance">
        <div>Custom content</div>
      </SettingsSection>
    );

    expect(screen.getByText('Appearance')).toBeInTheDocument();
    expect(screen.getByText('Custom content')).toBeInTheDocument();
  });
});

describe('SettingsHeader', () => {
  it('renders title and triggers close action', async () => {
    const onClose = jest.fn();
    const user = userEvent.setup();

    render(<SettingsHeader onClose={onClose} />);

    expect(screen.getByText('Configurações do sistema')).toBeInTheDocument();

    await user.click(screen.getByRole('button', { name: /close settings/i }));
    expect(onClose).toHaveBeenCalledTimes(1);
  });
});

describe('ModeSection', () => {
  it('marks the current mode and switches when a new one is selected', async () => {
    const setMode = jest.fn();
    const user = userEvent.setup();

    renderWithTheme(<ModeSection />, { mode: 'light', setMode });

    expect(screen.getByRole('button', { name: /light/i })).toHaveAttribute('aria-pressed', 'true');

    await user.click(screen.getByRole('button', { name: /dark/i }));
    expect(setMode).toHaveBeenCalledWith('dark');
  });

  it('ignores clicks that would unset the current selection', async () => {
    const setMode = jest.fn();
    const user = userEvent.setup();

    renderWithTheme(<ModeSection />, { mode: 'light', setMode });

    await user.click(screen.getByRole('button', { name: /light/i }));
    expect(setMode).not.toHaveBeenCalled();
  });
});

describe('SettingsDrawer', () => {
  it('renders the drawer content and propagates close actions', async () => {
    const onClose = jest.fn();
    const setMode = jest.fn();
    const user = userEvent.setup();

    renderWithTheme(<SettingsDrawer open onClose={onClose} />, { mode: 'dark', setMode });

    expect(screen.getByText('Configurações do sistema')).toBeInTheDocument();
    expect(screen.getByText(/mode/i)).toBeInTheDocument();

    await user.click(screen.getByRole('button', { name: /close settings/i }));
    expect(onClose).toHaveBeenCalledTimes(1);
  });
});
