import dev from './env/dev';
import hlg from './env/hlg';
import prod from './env/prod';

type AppEnv = 'dev' | 'hlg' | 'prod';

const env = (import.meta.env.VITE_APP_ENV as AppEnv) || 'dev';

const configs = { dev, hlg, prod } as const;

export const { API_BASE_URL } = configs[env];
