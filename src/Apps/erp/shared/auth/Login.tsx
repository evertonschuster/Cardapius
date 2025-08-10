import React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

import './Login.less';
import { useLogin } from './useLogin';

interface LoginProps {
  onLogin: (credentials: { username: string; password: string }) => void;
  onRecoverPassword: () => void;
  clientLogoUrl: string;
}

export const Login: React.FC<LoginProps> = ({ onLogin, onRecoverPassword, clientLogoUrl }) => {
  const {
    register,
    handleSubmit,
    errors,
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    session
  } = useLogin({ onLogin, onRecoverPassword });

  return (
    <Box className="login-container">
      <Box component="form" onSubmit={handleSubmit(onSubmit)} className="login-card">
        {clientLogoUrl && <img src={clientLogoUrl} alt="Logo do cliente" className="logo" />}
        <TextField
          label="Usuário"
          fullWidth
          margin="normal"
          {...register('username', { required: true })}
        />
        {errors.username && (
          <Typography color="error" variant="body2">
            Usuário é obrigatório
          </Typography>
        )}
        <TextField
          label="Senha"
          type={showPassword ? 'text' : 'password'}
          fullWidth
          margin="normal"
          {...register('password', { required: true })}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  onClick={toggleShowPassword}
                  aria-label={showPassword ? 'Ocultar senha' : 'Mostrar senha'}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            )
          }}
        />
        {errors.password && (
          <Typography color="error" variant="body2">
            Senha é obrigatória
          </Typography>
        )}
        <FormControlLabel control={<Checkbox {...register('remember')} />} label="Salvar senha" />
        <Button type="submit" variant="contained" fullWidth aria-label="Entrar no sistema">
          Entrar
        </Button>
        <Box mt={2}>
          <Link href="#" onClick={(e) => handleRecover(e)}>
            Esqueci minha senha
          </Link>
        </Box>
        <Typography className="shortcuts" variant="caption">
          Atalhos: Ctrl+Enter para entrar, Alt+R para recuperar senha, Alt+V para mostrar/ocultar senha
        </Typography>
        <Typography className="footer" variant="body2">
          Desenvolvido por{' '}
          <Link href="https://cardapius.com" target="_blank" rel="noopener noreferrer">
            Cardapius
          </Link>
        </Typography>
        <Typography className="support" variant="body2">
          suporte@cardapius.com
          <br />
          (11) 0000-0000
        </Typography>
        {session.token && (
          <Typography data-testid="session-info" variant="body2" mt={2}>
            Bem-vindo, {session.username}
          </Typography>
        )}
      </Box>
    </Box>
  );
};
