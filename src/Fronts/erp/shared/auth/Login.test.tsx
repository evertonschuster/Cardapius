import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { Login } from './Login';

describe('Login component', () => {
  test('renders required fields and shortcuts', () => {
    render(<Login onLogin={jest.fn()} onRecoverPassword={jest.fn()} clientLogoUrl="logo.png" />);
    expect(screen.getByLabelText(/Usuário/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Senha/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Entrar no sistema/i)).toBeInTheDocument();
    expect(screen.getByText(/Esqueci minha senha/i)).toBeInTheDocument();
    expect(screen.getByText(/Ctrl\+Enter/)).toBeInTheDocument();
  });

  test('toggles password visibility', async () => {
    render(<Login onLogin={jest.fn()} onRecoverPassword={jest.fn()} clientLogoUrl="logo.png" />);
    const toggle = screen.getByLabelText(/Mostrar senha/i);
    const password = screen.getByLabelText(/Senha/i) as HTMLInputElement;
    expect(password.type).toBe('password');
    await userEvent.click(toggle);
    expect(password.type).toBe('text');
  });

  test('triggers callbacks and shortcuts', async () => {
    const onLogin = jest.fn();
    const onRecover = jest.fn();
    const user = userEvent.setup();
    render(<Login onLogin={onLogin} onRecoverPassword={onRecover} clientLogoUrl="logo.png" />);
    await user.type(screen.getByLabelText(/Usuário/i), 'john');
    await user.type(screen.getByLabelText(/Senha/i), 'secret');
    await user.click(screen.getByLabelText(/Entrar no sistema/i));
    expect(onLogin).toHaveBeenCalledWith({ username: 'john', password: 'secret' });
    expect(screen.getByText(/Bem-vindo, john/i)).toBeInTheDocument();
    await user.click(screen.getByText(/Esqueci minha senha/i));
    expect(onRecover).toHaveBeenCalledTimes(1);
    await user.keyboard('{Control>}{Enter}{/Control}');
    expect(onLogin).toHaveBeenCalledTimes(2);
  });
});
