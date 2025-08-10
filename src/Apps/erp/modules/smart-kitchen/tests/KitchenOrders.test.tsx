import { render, screen } from '@testing-library/react';
import { KitchenOrders } from '../components/KitchenOrders';

test('renders smart kitchen orders placeholder', () => {
  render(<KitchenOrders />);
  expect(screen.getByText(/Smart Kitchen Lista de Pedidos TODO/i)).toBeInTheDocument();
});
