const readImportMetaEnv = (): Partial<Record<'VITE_APP_ENV', string>> => {
  try {
    return ((0, eval)('import.meta') as { env?: { VITE_APP_ENV?: string } })?.env ?? {};
  } catch {
    return {};
  }
};

export const getEnv = () => {
  const importMetaEnv = readImportMetaEnv();
  const processEnv = typeof process !== 'undefined' ? process.env ?? {} : {};

  return importMetaEnv.VITE_APP_ENV ?? processEnv.VITE_APP_ENV ?? 'dev';
};
