import React, { useCallback, useEffect, useState } from 'react';
import styles from './Login.module.css';

interface LoginProps {
  onLogin: (credentials: { username: string; password: string }) => void;
  onRecoverPassword: () => void;
  clientLogoUrl: string;
}

export const Login: React.FC<LoginProps> = ({ onLogin, onRecoverPassword, clientLogoUrl }) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [remember, setRemember] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const saved = localStorage.getItem('login_username');
    if (saved) {
      setUsername(saved);
      setRemember(true);
    }
  }, []);

  const toggleShowPassword = useCallback(() => {
    setShowPassword((prev) => !prev);
  }, []);

  const handleSubmit = useCallback(
    (e: React.FormEvent) => {
      e.preventDefault();
      if (!username || !password) {
        setError('Usuário e senha são obrigatórios.');
        return;
      }
      setError('');
      if (remember) {
        localStorage.setItem('login_username', username);
      } else {
        localStorage.removeItem('login_username');
      }
      onLogin({ username, password });
    },
    [username, password, remember, onLogin]
  );

  const handleRecover = useCallback(
    (e: React.MouseEvent<HTMLAnchorElement>) => {
      e.preventDefault();
      onRecoverPassword();
    },
    [onRecoverPassword]
  );

  return (
    <div className={styles.container}>
      <form className={styles.card} onSubmit={handleSubmit}>
        {clientLogoUrl && <img src={clientLogoUrl} alt="Logo do cliente" className={styles.logo} />}
        <div className={styles.formGroup}>
          <label htmlFor="username">Usuário</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Digite seu usuário"
          />
        </div>
        <div className={styles.formGroup}>
          <label htmlFor="password">Senha</label>
          <div className={styles.passwordWrapper}>
            <input
              id="password"
              type={showPassword ? 'text' : 'password'}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Digite sua senha"
            />
            <button
              type="button"
              className={styles.togglePassword}
              onClick={toggleShowPassword}
              aria-label={showPassword ? 'Ocultar senha' : 'Mostrar senha'}
            >
              {showPassword ? '🙈' : '👁️'}
            </button>
          </div>
        </div>
        {error && <div className={styles.error}>{error}</div>}
        <div className={styles.formGroup}>
          <label className={styles.checkbox}>
            <input
              type="checkbox"
              checked={remember}
              onChange={(e) => setRemember(e.target.checked)}
            />
            Salvar senha
          </label>
        </div>
        <button type="submit" className={styles.submit} aria-label="Entrar no sistema">
          Entrar
        </button>
        <div className={styles.links}>
          <a href="#" onClick={handleRecover}>
            Esqueci minha senha
          </a>
        </div>
        <div className={styles.footer}>
          Desenvolvido por{' '}
          <a href="https://cardapius.com" target="_blank" rel="noopener noreferrer">
            Cardapius
          </a>
        </div>
        <div className={styles.support}>
          <div>suporte@cardapius.com</div>
          <div>(11) 0000-0000</div>
        </div>
      </form>
    </div>
  );
};
