import React, { useEffect, useRef } from 'react';
import MuiButton, { ButtonProps } from '@mui/material/Button';
import Typography from '@mui/material/Typography';

interface ShortcutButtonProps extends ButtonProps {
  shortcut?: string[];
}

export const Button: React.FC<ShortcutButtonProps> = ({
  shortcut,
  children,
  ...props
}) => {
  const ref = useRef<HTMLButtonElement>(null);

  useEffect(() => {
    if (!shortcut || shortcut.length === 0) return;
    const handler = (e: KeyboardEvent) => {
      const keys = shortcut
        .filter((k): k is string => Boolean(k))
        .map((k) => k.toLowerCase());
      const ctrl = keys.includes('ctrl');
      const alt = keys.includes('alt');
      const shift = keys.includes('shift');
      const key = keys.find(
        (k) => k !== 'ctrl' && k !== 'alt' && k !== 'shift'
      );
      if (
        key &&
        e.key.toLowerCase() === key &&
        e.ctrlKey === ctrl &&
        e.altKey === alt &&
        e.shiftKey === shift
      ) {
        e.preventDefault();
        ref.current?.click();
      }
    };
    window.addEventListener('keydown', handler);
    return () => window.removeEventListener('keydown', handler);
  }, [shortcut]);

  const shortcutLabel = (shortcut ?? []).slice(1).join('+');

  return (
    <MuiButton ref={ref} {...props}>
      {children}
      {shortcutLabel && (
        <Typography
          variant="caption"
          component="span"
          sx={{
            ml: 0.5,
            opacity: 0.75,
            fontSize: '0.45rem',
          }}
        >
          ({shortcutLabel})
        </Typography>
      )}
    </MuiButton>
  );
};
