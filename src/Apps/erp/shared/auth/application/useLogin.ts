import { useCallback, useEffect, useReducer, useState, MouseEvent } from 'react';
import { useForm } from 'react-hook-form';
import { authService } from '../infrastructure/authService';

interface SessionState {
  username: string;
  roles: string[];
  token: string;
}

interface LoginForm {
  username: string;
  password: string;
  remember: boolean;
}

type SessionAction = { type: 'SET'; payload: SessionState } | { type: 'CLEAR' };

const sessionReducer = (state: SessionState, action: SessionAction): SessionState => {
  switch (action.type) {
    case 'SET':
      return action.payload;
    case 'CLEAR':
      return { username: '', roles: [], token: '' };
    default:
      return state;
  }
};

export const useLogin = () => {
  const [, dispatch] = useReducer(sessionReducer, {
    username: '',
    roles: [],
    token: ''
  });
  const [showPassword, setShowPassword] = useState(false);

  const { control, handleSubmit, setValue, watch } = useForm<LoginForm>({
    defaultValues: { username: '', password: '', remember: false }
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
      if (e.ctrlKey && e.key === 'Enter') {
        handleSubmit(onSubmit)();
      }
      if (e.altKey && e.key.toLowerCase() === 'r') {
        handleRecover();
      }
      if (e.altKey && e.key.toLowerCase() === 'v') {
        toggleShowPassword();
      }
    };
    window.addEventListener('keydown', handler);
    return () => window.removeEventListener('keydown', handler);
  }, [handleSubmit, onSubmit, handleRecover, toggleShowPassword]);

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

export type { SessionState };
