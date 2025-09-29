import { getEnv } from './index';

describe('getEnv', () => {
  const originalEnv = { ...process.env };

  beforeEach(() => {
    for (const key of Object.keys(process.env)) {
      delete process.env[key];
    }

    Object.assign(process.env, originalEnv);
    delete process.env.VITE_APP_ENV;
  });

  afterAll(() => {
    for (const key of Object.keys(process.env)) {
      delete process.env[key];
    }

    Object.assign(process.env, originalEnv);
  });

  it('returns default dev when env is missing', () => {
    expect(getEnv()).toBe('dev');
  });

  it('returns the configured environment value', () => {
    process.env.VITE_APP_ENV = 'prod';
    expect(getEnv()).toBe('prod');
  });
});
