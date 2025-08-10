import { ProductCache } from './productCache';

it('stores and retrieves products', async () => {
  const cache = new ProductCache();
  await cache.cacheProducts([{ id: '1', name: 'Test', quantity: 1 }]);
  const products = await cache.getProducts();
  expect(products.length).toBeGreaterThan(0);
});
