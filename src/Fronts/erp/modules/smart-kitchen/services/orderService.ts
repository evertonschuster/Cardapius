import { Order } from '@shared/types';
import { API_BASE_URL } from '@shared/config';

export const fetchOrders = async (): Promise<Order[]> => {
  const url = `${API_BASE_URL}/orders`;
  // TODO: real-time orders fetching using the URL above
  return [];
};
