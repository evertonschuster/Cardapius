import React from 'react';
import { Controller, Control, FieldValues } from 'react-hook-form';
import Checkbox, { CheckboxProps } from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';

interface RHFCheckboxProps<T extends FieldValues> {
  name: string;
  control: Control<T>;
  label: string;
  checkboxProps?: CheckboxProps;
}

export const RHFCheckbox = <T extends FieldValues>({
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
        control={<Checkbox {...field} checked={field.value} {...checkboxProps} />}
        label={label}
      />
    )}
  />
);
