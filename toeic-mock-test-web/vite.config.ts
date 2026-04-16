import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path' // Cài thêm: npm install -D @types/node

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
})