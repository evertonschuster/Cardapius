import * as yup from 'yup';

export const loginValidationSchema = yup.object({
  username: yup.string().required('Usuário é obrigatório'),
  password: yup.string().required('Senha é obrigatória'),
  remember: yup.boolean()
});
