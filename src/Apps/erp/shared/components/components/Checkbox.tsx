import React from 'react';
import { Controller, Control, FieldValues } from 'react-hook-form';
import CheckboxMUI, { CheckboxProps } from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';

interface RHFCheckboxProps<T extends FieldValues> {
  name: string;
  control: Control<T>;
  label: string;
  checkboxProps?: CheckboxProps;
}

export const Checkbox = <T extends FieldValues>({
  name,
  control,
  label,
  checkboxProps
}: RHFCheckboxProps<T>) => (
  <Controller
    name={name}
    control={control}
    render={({ field }) => (
      <FormControlLabel
        control={<CheckboxMUI {...field} checked={field.value} {...checkboxProps} />}
        label={label}
      />
    )}
  />
);
