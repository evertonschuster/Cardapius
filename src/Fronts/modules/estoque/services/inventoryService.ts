import { Product } from '@shared/types';

export const updateQuantity = (product: Product, quantity: number): Product => {
  return { ...product, quantity };
};
