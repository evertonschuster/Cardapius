import { fetchOrders } from '../services/orderService';

test('fetchOrders returns empty array by default', async () => {
  await expect(fetchOrders()).resolves.toEqual([]);
});
