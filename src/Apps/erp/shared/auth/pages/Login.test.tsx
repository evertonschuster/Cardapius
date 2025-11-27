import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { Login } from './Login';
import { useAuth } from '../AuthProvider';

jest.mock('../AuthProvider', () => ({ useAuth: jest.fn() }));
jest.mock('../components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div>{title}</div>,
}));
jest.mock('../components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: any) => <div>error:{details.title}</div>,
}));

const mockUseAuth = useAuth as jest.Mock;

describe('Login page', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('calls signin on mount and shows loading state', async () => {
    const signin = jest.fn().mockResolvedValue(undefined);
    mockUseAuth.mockReturnValue({ signin });

    render(<Login />);

    await waitFor(() => expect(signin).toHaveBeenCalledTimes(1));
    expect(screen.getByText('Processando informações de login...')).toBeInTheDocument();
  });

  it('renders error details when signin returns an error', async () => {
    const signin = jest.fn().mockResolvedValue({ title: 'signin-error' });
    mockUseAuth.mockReturnValue({ signin });

    render(<Login />);

    expect(await screen.findByText('error:signin-error')).toBeInTheDocument();
    expect(signin).toHaveBeenCalledTimes(1);
  });
});
