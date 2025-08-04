import { authService } from './authService';

test('login returns tokens and roles', async () => {
  const res = await authService.login('user', 'pass');
  expect(res.accessToken).toBeTruthy();
  expect(res.refreshToken).toBeTruthy();
  expect(res.roles.length).toBeGreaterThan(0);
});
