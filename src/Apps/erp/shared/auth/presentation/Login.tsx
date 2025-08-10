import React from 'react';
import Box from '@mui/material/Box';
import { Button } from '../../components/Button';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
const apiUrl = import.meta.env;

import { useLogin } from '../application/useLogin';
import { TextField } from '../../components/TextField';
import { Checkbox } from '../../components/Checkbox';


import './Login.less';
import { useForm } from 'react-hook-form';
import { Form } from './components/Form';
import { FormProvider } from '@shared/components/FormProvider';

export const Login: React.FC = () => {
  const {
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    clientLogoUrl
  } = useLogin();

  console.log(apiUrl)
  const { control, handleSubmit, setValue, watch } = useForm<any>({
    defaultValues: { username: '', password: '', remember: false },
  });

  return (
    <Box className="login-container">
      <Box component="form" onSubmit={handleSubmit(onSubmit)} className="login-card">
        {clientLogoUrl && <img src={clientLogoUrl} alt="Logo do cliente" className="logo" />}

        <FormProvider >
          <Form />
        </FormProvider>
        
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
