import React from 'react';
import { FieldValues, RegisterOptions, useController, useFormContext } from 'react-hook-form';
import TextFieldMUI, { TextFieldProps } from '@mui/material/TextField';

interface RHFTextFieldProps<T extends FieldValues>
  extends Omit<TextFieldProps, 'name' | 'defaultValue'> {
  name: string;
  rules?: RegisterOptions;
}

export const TextField = <T extends FieldValues>({
  name,
  rules,
  ...props
}: RHFTextFieldProps<T>) => {
  const { field, fieldState: { error } } = useController({ name, rules });
  return (

    <TextFieldMUI {...field} {...props} error={!!error} helperText={error?.message} />
  );
};
