import { render, screen } from '@testing-library/react';
import { InventoryOverview } from '../components/InventoryOverview';

test('renders inventory overview placeholder', () => {
  render(<InventoryOverview />);
  expect(screen.getByText(/Estoque Visão Geral TODO/i)).toBeInTheDocument();
});
