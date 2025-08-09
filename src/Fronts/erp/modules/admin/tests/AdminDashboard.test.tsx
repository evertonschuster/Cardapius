import { render, screen } from '@testing-library/react';
import { AdminDashboard } from '../components/AdminDashboard';

test('renders admin dashboard placeholder', () => {
  render(<AdminDashboard />);
  expect(screen.getByText(/Admin Dashboard TODO/i)).toBeInTheDocument();
});
