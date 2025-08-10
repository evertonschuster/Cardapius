import React from 'react';
import Box from '@mui/material/Box';
import { Button } from '../../components/components/Button';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

import { useLogin } from '../application/useLogin';
import { TextField } from '../../components/components/TextField';
import { Checkbox } from '../../components/components/Checkbox';


import './Login.less';

export const Login: React.FC = () => {
  const {
    control,
    handleSubmit,
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    clientLogoUrl
  } = useLogin();

  return (
    <Box className="login-container">
      <Box component="form" onSubmit={handleSubmit(onSubmit)} className="login-card">
        {clientLogoUrl && <img src={clientLogoUrl} alt="Logo do cliente" className="logo" />}
        <TextField
          name="username"
          control={control}
          label="Usuário"
          fullWidth
          margin="normal"
        />
        <TextField
          name="password"
          control={control}
          label="Senha"
          type={showPassword ? 'text' : 'password'}
          fullWidth
          margin="normal"
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
        <Checkbox name="remember" control={control} label="Salvar senha" />
        <Button
          type="submit"
          variant="contained"
          fullWidth
          aria-label="Entrar no sistema"
          shortcut={['Ctrl', 'Enter']}
        >
          Entrar
        </Button>
        <Box mt={2}>
          <Link href="#" onClick={handleRecover}>
            Esqueci minha senha
          </Link>
        </Box>
        
        <Typography className="footer" variant="body2">
          Desenvolvido por{' '}
          <Link href="https://cardapius.com.br" target="_blank" rel="noopener noreferrer">
            Cardapius
          </Link>
        </Typography>
        <Typography className="support" variant="body2">
          suporte@cardapius.com.br
          <br />
          (45) 99934-2864
        </Typography>
      </Box>
    </Box>
  );
};
