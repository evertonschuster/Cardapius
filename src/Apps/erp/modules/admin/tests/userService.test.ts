import { fetchUsers } from '../services/userService';

test('fetchUsers returns empty array by default', async () => {
  await expect(fetchUsers()).resolves.toEqual([]);
});
