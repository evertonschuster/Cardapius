import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { Button } from '../Button';

test('triggers click on shortcut and shows label', async () => {
  const user = userEvent.setup();
  const handleClick = jest.fn();
  render(
    <Button shortcut={['Ctrl', 'Enter']} onClick={handleClick}>
      Save
    </Button>
  );
  await user.keyboard('{Control>}{Enter}{/Control}');
  expect(handleClick).toHaveBeenCalled();
  expect(
    screen.getByRole('button', { name: /Save \(Ctrl\+Enter\)/i })
  ).toBeInTheDocument();
});
