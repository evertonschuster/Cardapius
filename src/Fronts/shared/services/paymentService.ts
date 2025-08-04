export interface PaymentRequest {
  amount: number;
}

export interface PaymentResponse {
  success: boolean;
}

// Simulated abstraction for card machine service
export const processPayment = async (_req: PaymentRequest): Promise<PaymentResponse> => {
  // TODO: integrate with real payment SDK
  return { success: true };
};
