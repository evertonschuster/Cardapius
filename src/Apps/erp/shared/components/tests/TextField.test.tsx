import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { useForm } from 'react-hook-form';
import { TextField } from '../TextField';

test('renders text field and updates value', async () => {
  const Wrapper = () => {
    const { control } = useForm({ defaultValues: { name: '' } });
    return <TextField name="name" control={control} label="Name" />;
  };
  const user = userEvent.setup();
  render(<Wrapper />);
  const input = screen.getByLabelText('Name');
  await user.type(input, 'John');
  expect((input as HTMLInputElement).value).toBe('John');
});
