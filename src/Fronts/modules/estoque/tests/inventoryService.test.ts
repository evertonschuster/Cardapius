import { updateQuantity } from '../services/inventoryService';
import { Product } from '@shared/types';

test('updateQuantity updates product quantity', () => {
  const product: Product = { id: '1', name: 'Item', quantity: 1 };
  expect(updateQuantity(product, 5).quantity).toBe(5);
});
