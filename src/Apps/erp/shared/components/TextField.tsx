import React from 'react';
import {
  Control,
  FieldValues,
  Path,
  RegisterOptions,
  useController,
} from 'react-hook-form';
import TextFieldMUI, { TextFieldProps } from '@mui/material/TextField';

interface RHFTextFieldProps<T extends FieldValues>
  extends Omit<TextFieldProps, 'name' | 'defaultValue'> {
  name: Path<T>;
  rules?: RegisterOptions<T, Path<T>>;
  control: Control<T>;
}

export const TextField = <T extends FieldValues>({
  name,
  rules,
  control,
  ...props
}: RHFTextFieldProps<T>) => {
  const {
    field,
    fieldState: { error },
  } = useController<T>({ name, rules, control });
  return (

    <TextFieldMUI {...field} {...props} error={!!error} helperText={error?.message} />
  );
};
