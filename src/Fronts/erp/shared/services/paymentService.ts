export interface PaymentRequest {
  amount: number;
}

export interface PaymentResponse {
  success: boolean;
}

import { API_BASE_URL } from '@shared/config';

// Simulated abstraction for card machine service
export const processPayment = async (_req: PaymentRequest): Promise<PaymentResponse> => {
  const url = `${API_BASE_URL}/payments`;
  // TODO: integrate with real payment SDK or POST to the URL above
  return { success: true };
};
