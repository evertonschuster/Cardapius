import { useCallback, useEffect, useReducer, useState, MouseEvent } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { authService } from '../infrastructure/authService';
import { sessionReducer, initialSessionState } from './session';
import { loginValidationSchema } from './validation';

interface LoginForm {
  username: string;
  password: string;
  remember: boolean;
}

export const useLogin = () => {
  const [, dispatch] = useReducer(sessionReducer, initialSessionState);
  const [showPassword, setShowPassword] = useState(false);

  const { control, handleSubmit, setValue, watch } = useForm<LoginForm>({
    defaultValues: { username: '', password: '', remember: false },
    resolver: yupResolver(loginValidationSchema)
  });

  useEffect(() => {
    const saved = localStorage.getItem('login_username');
    if (saved) {
      setValue('username', saved);
      setValue('remember', true);
    }
  }, [setValue]);

  watch('remember');

  const toggleShowPassword = useCallback(() => {
    setShowPassword((prev) => !prev);
  }, []);

  const onSubmit = useCallback(
    async (data: LoginForm) => {
      if (data.remember) {
        localStorage.setItem('login_username', data.username);
      } else {
        localStorage.removeItem('login_username');
      }
      const res = await authService.login(data.username, data.password);
      dispatch({
        type: 'SET',
        payload: {
          username: data.username,
          roles: res.roles,
          token: res.accessToken
        }
      });
    },
    []
  );

  const handleRecover = useCallback(
    (e?: MouseEvent<HTMLAnchorElement>) => {
      if (e) e.preventDefault();
      // TODO: implement password recovery flow
    },
    []
  );

  useEffect(() => {
    const handler = (e: KeyboardEvent) => {
      if (e.altKey && e.key.toLowerCase() === 'r') {
        handleRecover();
      }
      if (e.altKey && e.key.toLowerCase() === 'v') {
        toggleShowPassword();
      }
    };
    window.addEventListener('keydown', handler);
    return () => window.removeEventListener('keydown', handler);
  }, [handleRecover, toggleShowPassword]);

  const clientLogoUrl = '/logo.png';

  return {
    control,
    handleSubmit,
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    clientLogoUrl
  };
};
