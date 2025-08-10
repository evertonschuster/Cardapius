import { Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, IconButton, InputAdornment, Link } from '@mui/material';
import { useLogin } from '@shared/auth/application/useLogin';
import { Button } from '@shared/components/Button';
import { TextField } from '@shared/components/TextField'
import React from 'react'

export const Form = () => {
      const {
        onSubmit,
        toggleShowPassword,
        handleRecover,
        showPassword,
        clientLogoUrl
      } = useLogin();
      
    return (
        <>
            <TextField
                name="username"
                label="Usuário"
                fullWidth
                margin="normal"
            />
            <TextField
                name="password"
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
            {/* <Checkbox name="remember" control={control} label="Salvar senha" /> */}
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
        </>
    )
}
