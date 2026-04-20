import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import path from 'path';
import fs from 'fs';

export default defineConfig(({ command }) => {
  const keyPath = path.resolve(__dirname, 'localhost-key.pem');
  const certPath = path.resolve(__dirname, 'localhost.pem');
  const hasCerts = command === 'serve' && fs.existsSync(keyPath) && fs.existsSync(certPath);

  return {
    plugins: [vue()],
    server: {
      host: true,
      https: hasCerts ? { key: fs.readFileSync(keyPath), cert: fs.readFileSync(certPath) } : undefined,
    },
    resolve: {
      alias: {
        '@': path.resolve(__dirname, './src'),
      },
    },
  };
});
