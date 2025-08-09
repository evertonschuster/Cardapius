import { calculateTotal } from '../services/totalService';

test('calculateTotal sums values', () => {
  expect(calculateTotal([1, 2, 3])).toBe(6);
});
