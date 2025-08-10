import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { useForm } from 'react-hook-form';
import { Checkbox } from '../Checkbox';

test('renders checkbox and toggles value', async () => {
  const Wrapper = () => {
    const { control } = useForm({ defaultValues: { remember: false } });
    return <Checkbox name="remember" control={control} label="Remember" />;
  };
  const user = userEvent.setup();
  render(<Wrapper />);
  const checkbox = screen.getByLabelText('Remember');
  expect(checkbox).not.toBeChecked();
  await user.click(checkbox);
  expect(checkbox).toBeChecked();
});
