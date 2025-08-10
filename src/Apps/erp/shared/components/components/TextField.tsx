import React from 'react';
import { Controller, Control, FieldValues, RegisterOptions } from 'react-hook-form';
import TextFieldMUI, { TextFieldProps } from '@mui/material/TextField';

interface RHFTextFieldProps<T extends FieldValues>
  extends Omit<TextFieldProps, 'name' | 'defaultValue'> {
  name: string;
  control: Control<T>;
  rules?: RegisterOptions;
}

export const TextField = <T extends FieldValues>({
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
      <TextFieldMUI {...field} {...props} error={!!error} helperText={error?.message} />
    )}
  />
);
