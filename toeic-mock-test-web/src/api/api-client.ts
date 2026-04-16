import axios from 'axios';

// Đây là cấu hình chung
export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7295/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor: Tự động gắn Token vào mỗi khi gọi API (nếu có)
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor: Xử lý lỗi tập trung (ví dụ 401 thì văng ra trang login)
apiClient.interceptors.response.use(
  (response) => response.data, // Trả về thẳng data để bên ngoài không cần .data nữa
  (error) => {
    // Ông có thể xử lý lỗi 401, 500 ở đây
    return Promise.reject(error);
  }
);