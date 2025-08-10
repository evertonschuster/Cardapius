import { render, screen } from '@testing-library/react';
import { PdvSales } from '../components/PdvSales';

test('renders pdv sales placeholder', () => {
  render(<PdvSales />);
  expect(screen.getByText(/PDV Venda Rápida TODO/i)).toBeInTheDocument();
});
