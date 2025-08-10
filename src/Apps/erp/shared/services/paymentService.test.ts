import { processPayment, PaymentRequest } from './paymentService';

test('processPayment resolves successfully', async () => {
  const req: PaymentRequest = { amount: 100 };
  await expect(processPayment(req)).resolves.toEqual({ success: true });
});
