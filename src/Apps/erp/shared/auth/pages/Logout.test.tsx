import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { Logout } from './Logout';
import { useAuth } from '../AuthProvider';

jest.mock('../AuthProvider', () => ({ useAuth: jest.fn() }));
jest.mock('../components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div>{title}</div>,
}));
jest.mock('../components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: any) => <div>error:{details.title}</div>,
}));

const mockUseAuth = useAuth as jest.Mock;

describe('Logout page', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('calls signout on mount and shows loading state', async () => {
    const signout = jest.fn().mockResolvedValue(undefined);
    mockUseAuth.mockReturnValue({ signout });

    render(<Logout />);

    await waitFor(() => expect(signout).toHaveBeenCalledTimes(1));
    expect(screen.getByText('Processando logout...')).toBeInTheDocument();
  });

  it('renders error details when signout returns an error', async () => {
    const signout = jest.fn().mockResolvedValue({ title: 'signout-error' });
    mockUseAuth.mockReturnValue({ signout });

    render(<Logout />);

    expect(await screen.findByText('error:signout-error')).toBeInTheDocument();
    expect(signout).toHaveBeenCalledTimes(1);
  });
});
