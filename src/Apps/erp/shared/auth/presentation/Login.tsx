import React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

import './Login.less';
import { useLogin } from '../application/useLogin';
import { RHFTextField } from './components/RHFTextField';
import { RHFCheckbox } from './components/RHFCheckbox';

export const Login: React.FC = () => {
  const {
    control,
    handleSubmit,
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    session,
    clientLogoUrl
  } = useLogin();

  return (
    <Box className="login-container">
      <Box component="form" onSubmit={handleSubmit(onSubmit)} className="login-card">
        {clientLogoUrl && <img src={clientLogoUrl} alt="Logo do cliente" className="logo" />}
        <RHFTextField
          name="username"
          control={control}
          label="Usuário"
          fullWidth
          margin="normal"
          rules={{ required: 'Usuário é obrigatório' }}
        />
        <RHFTextField
          name="password"
          control={control}
          label="Senha"
          type={showPassword ? 'text' : 'password'}
          fullWidth
          margin="normal"
          rules={{ required: 'Senha é obrigatória' }}
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
        <RHFCheckbox name="remember" control={control} label="Salvar senha" />
        <Button type="submit" variant="contained" fullWidth aria-label="Entrar no sistema">
          Entrar
        </Button>
        <Box mt={2}>
          <Link href="#" onClick={handleRecover}>
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
