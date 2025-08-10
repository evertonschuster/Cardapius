import React from 'react'
import { useForm, FormProvider as FormProviderRHF } from 'react-hook-form'

interface FormProviderProps {
    children: React.ReactNode | React.ReactNode[];
}
export const FormProvider: React.FC<FormProviderProps> = ({ children }) => {

    const methods = useForm()
    return (
        <FormProviderRHF {...methods}>
            {children}
        </FormProviderRHF>
    )
}
