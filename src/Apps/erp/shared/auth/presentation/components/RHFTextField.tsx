import React from 'react';
import { Controller, Control, FieldValues, RegisterOptions } from 'react-hook-form';
import TextField, { TextFieldProps } from '@mui/material/TextField';

interface RHFTextFieldProps<T extends FieldValues>
  extends Omit<TextFieldProps, 'name' | 'defaultValue'> {
  name: string;
  control: Control<T>;
  rules?: RegisterOptions;
}

export const RHFTextField = <T extends FieldValues>({
  name,
  control,
  rules,
  ...props
}: RHFTextFieldProps<T>) => (
  <Controller
    name={name}
    control={control}
    rules={rules}
    render={({ field, fieldState: { error } }) => (
      <TextField {...field} {...props} error={!!error} helperText={error?.message} />
    )}
  />
);
