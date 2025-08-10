import { useCallback, useEffect, useReducer, useState } from 'react';
import { useForm } from 'react-hook-form';

interface UseLoginParams {
  onLogin: (credentials: { username: string; password: string }) => void;
  onRecoverPassword: () => void;
}

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

export const useLogin = ({ onLogin, onRecoverPassword }: UseLoginParams) => {
  const [session, dispatch] = useReducer(sessionReducer, {
    username: '',
    roles: [],
    token: ''
  });
  const [showPassword, setShowPassword] = useState(false);

  const {
    register,
    handleSubmit,
    setValue,
    watch,
    formState: { errors }
  } = useForm<LoginForm>({
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
    (data: LoginForm) => {
      if (data.remember) {
        localStorage.setItem('login_username', data.username);
      } else {
        localStorage.removeItem('login_username');
      }
      onLogin({ username: data.username, password: data.password });
      dispatch({
        type: 'SET',
        payload: {
          username: data.username,
          roles: ['user'],
          token: 'mock-token'
        }
      });
    },
    [onLogin]
  );

  const handleRecover = useCallback(
    (e?: React.MouseEvent<HTMLAnchorElement>) => {
      if (e) e.preventDefault();
      onRecoverPassword();
    },
    [onRecoverPassword]
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

  return {
    register,
    handleSubmit,
    errors,
    onSubmit,
    toggleShowPassword,
    handleRecover,
    showPassword,
    session
  };
};

export type { SessionState };
