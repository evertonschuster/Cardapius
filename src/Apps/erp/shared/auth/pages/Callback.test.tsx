import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { Callback } from './Callback';
import { useAuth } from '../AuthProvider';
import { useNavigate } from 'react-router-dom';

jest.mock('../AuthProvider', () => ({ useAuth: jest.fn() }));
jest.mock('../components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div>{title}</div>,
}));
jest.mock('../components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: any) => <div>error:{details.title}</div>,
}));
jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: jest.fn(),
}));

const mockUseAuth = useAuth as jest.Mock;
const mockUseNavigate = useNavigate as jest.Mock;

describe('Callback page', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('renders loading when already authenticated', () => {
    mockUseAuth.mockReturnValue({
      isAuthenticated: true,
      signinCallback: jest.fn(),
      signin: jest.fn(),
    });

    render(<Callback />);
    expect(screen.getByText(/Carregando informações/i)).toBeInTheDocument();
  });

  it('navigates after successful callback', async () => {
    const navigate = jest.fn();
    mockUseNavigate.mockReturnValue(navigate);
    mockUseAuth.mockReturnValue({
      isAuthenticated: false,
      signinCallback: jest.fn().mockResolvedValue({ redirectTo: '/home' }),
      signin: jest.fn(),
    });

    render(<Callback />);

    await waitFor(() => expect(navigate).toHaveBeenCalledWith('/home'));
  });

  it('shows error details on callback failure', async () => {
    mockUseNavigate.mockReturnValue(jest.fn());
    mockUseAuth.mockReturnValue({
      isAuthenticated: false,
      signinCallback: jest.fn().mockResolvedValue({ error: { title: 'bad' } }),
      signin: jest.fn(),
    });

    render(<Callback />);

    await waitFor(() => expect(screen.getByText('error:bad')).toBeInTheDocument());
  });
});
