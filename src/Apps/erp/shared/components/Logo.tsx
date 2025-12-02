import { Stack, Typography } from '@mui/material'
import React from 'react'
import logoImg from '../assets/logo.png'

type LogoProps = {
    showText?: boolean;
};

export const Logo: React.FC<LogoProps> = ({ showText = true }) => {
    return (
        <Stack
            direction="row"
            alignItems={"center"}
            p={1}
        >
            <img
                width={40}
                src={logoImg}
                alt="Cardapius logo"
            />
            {showText && (
                <Typography
                    p={1}
                    variant="subtitle2"
                >
                    Cardapius
                </Typography>
            )}
        </Stack >
    )
}
