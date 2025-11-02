import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import tsconfigPaths from 'vite-tsconfig-paths'

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '') // lê .env.* também sem exigir prefixo VITE_
  const port = Number(env.PORT) || 3000

  return {
    plugins: [react(), tsconfigPaths()],
    server: {
      port,
      strictPort: true,
      host: true,   
    },
    preview: {
      port: Number(env.PREVIEW_PORT) || 5000
    }
  }
})
