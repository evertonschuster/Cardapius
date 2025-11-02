import dev from './env/dev';
import hlg from './env/hlg';
import prod from './env/prod';

type AppEnv = 'dev' | 'hlg' | 'prod';

const readImportMetaEnv = (): Partial<Record<'VITE_APP_ENV', string>> => {
  try {
    return ((0, eval)('import.meta') as { env?: { VITE_APP_ENV?: string } })?.env ?? {};
  } catch (error) {
    return {};
  }
};

const env = (
  readImportMetaEnv().VITE_APP_ENV ??
  (typeof process !== 'undefined' ? process.env?.VITE_APP_ENV : undefined) ??
  'dev'
) as AppEnv;

const configs = { dev, hlg, prod } as const;

export const { API_BASE_URL } = configs[env];
