import { Product } from '@shared/types';
import { ProductCache } from '@shared/services';

const cache = new ProductCache();

export const updateQuantity = (product: Product, quantity: number): Product => {
  return { ...product, quantity };
};

export const cacheProducts = async (products: Product[]): Promise<void> => {
  await cache.cacheProducts(products);
};

export const getCachedProducts = async (): Promise<Product[]> => {
  return cache.getProducts();
};
