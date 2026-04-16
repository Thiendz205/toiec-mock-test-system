/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_URL: string;
  // Thêm các biến khác của ông ở đây nếu cần...
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}