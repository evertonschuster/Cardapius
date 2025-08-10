import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { Login } from './Login';
import { authService } from '../infrastructure/authService';

jest.mock('../infrastructure/authService', () => ({
  authService: {
    login: jest.fn().mockResolvedValue({
      accessToken: 'token',
      refreshToken: 'refresh',
      roles: ['user']
    })
  }
}));

describe('Login component', () => {
  test('renders required fields and shortcuts', () => {
    render(<Login />);
    expect(screen.getByLabelText(/Usuário/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Senha/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Entrar no sistema/i)).toBeInTheDocument();
    expect(screen.getByText(/Esqueci minha senha/i)).toBeInTheDocument();
    expect(screen.getByText(/Ctrl\+Enter/)).toBeInTheDocument();
  });

  test('toggles password visibility', async () => {
    render(<Login />);
    const toggle = screen.getByLabelText(/Mostrar senha/i);
    const password = screen.getByLabelText(/Senha/i) as HTMLInputElement;
    expect(password.type).toBe('password');
    await userEvent.click(toggle);
    expect(password.type).toBe('text');
  });

  test('submits form and triggers shortcuts', async () => {
    const user = userEvent.setup();
    render(<Login />);
    await user.type(screen.getByLabelText(/Usuário/i), 'john');
    await user.type(screen.getByLabelText(/Senha/i), 'secret');
    await user.click(screen.getByLabelText(/Entrar no sistema/i));
    await waitFor(() => expect(authService.login).toHaveBeenCalledWith('john', 'secret'));
    expect(await screen.findByText(/Bem-vindo, john/i)).toBeInTheDocument();
    await user.keyboard('{Control>}{Enter}{/Control}');
    await waitFor(() => expect(authService.login).toHaveBeenCalledTimes(2));
  });
});
