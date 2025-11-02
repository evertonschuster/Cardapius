import React from 'react';
import { render, screen } from '@testing-library/react';
import { Callback } from './Callback';
import { useAuth } from '../AuthProvider';

jest.mock('../AuthProvider', () => ({ useAuth: jest.fn() }));
jest.mock('../components/LoadProgressPage', () => ({
  LoadProgressPage: ({ title }: { title: string }) => <div>{title}</div>,
}));
jest.mock('../components/ProcessErrorDetails', () => ({
  ProcessErrorDetails: ({ details }: any) => <div>error:{details.title}</div>,
}));

const mockUseAuth = useAuth as jest.Mock;

describe('Callback', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('calls signinCallback on mount when user not present', () => {
    const signinCallback = jest.fn();
    mockUseAuth.mockReturnValue({ signinCallback, signin: jest.fn(), error: null, isLoading: false, user: null });
    render(<Callback />);
    expect(signinCallback).toHaveBeenCalled();
    expect(screen.getByText('Carregando informações...')).toBeInTheDocument();
  });

  it('renders error when error is present', () => {
    mockUseAuth.mockReturnValue({ signinCallback: jest.fn(), signin: jest.fn(), error: { title: 'err' }, isLoading: false, user: null });
    render(<Callback />);
    expect(screen.getByText('error:err')).toBeInTheDocument();
  });

  it('does not call signinCallback when user is already authenticated', () => {
    const signinCallback = jest.fn();
    mockUseAuth.mockReturnValue({ signinCallback, signin: jest.fn(), error: null, isLoading: false, user: {} });
    render(<Callback />);
    expect(signinCallback).not.toHaveBeenCalled();
  });
});
