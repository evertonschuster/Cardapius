import { getEnv } from './index';

const ensureImportMetaEnv = () => {
  if (!(import.meta as any).env) {
    (import.meta as any).env = {};
  }
};

describe('getEnv', () => {
  let originalEnv: any;

  beforeEach(() => {
    ensureImportMetaEnv();
    originalEnv = (import.meta as any).env;
  });

  afterEach(() => {
    (import.meta as any).env = originalEnv;
  });

  it('returns default dev when env is missing', () => {
    (import.meta as any).env = {};
    expect(getEnv()).toBe('dev');
  });

  it('returns the configured environment value', () => {
    (import.meta as any).env = { VITE_APP_ENV: 'prod' };
    expect(getEnv()).toBe('prod');
  });
});
