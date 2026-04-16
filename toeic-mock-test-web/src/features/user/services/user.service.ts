import { apiClient } from '@/api/api-client'; 
import { RecordStatusType } from '@/types/common.type';
import { 
  CreateUserRequest, 
  UpdateUserRequest, 
  UserResponse, 
  UserDetailResponse 
} from '../types';

export const userService = {
  // POST: api/Users
  create: (data: CreateUserRequest): Promise<UserResponse> => {
    return apiClient.post('/Users', data);
  },

  // GET: api/Users?status=1
  getAll: (status?: RecordStatusType): Promise<UserResponse[]> => {
    return apiClient.get('/Users', { 
      params: { status } 
    });
  },

  // GET: api/Users/{id}
  getById: (id: string): Promise<UserDetailResponse> => {
    return apiClient.get(`/Users/${id}`);
  },

  // PUT: api/Users/admin-update
  adminUpdate: (data: UpdateUserRequest): Promise<void> => {
    return apiClient.put('/Users/admin-update', data);
  },

  // PUT: api/Users/{id}/lock
  lock: (id: string): Promise<void> => {
    return apiClient.put(`/Users/${id}/lock`);
  },

  // PUT: api/Users/{id}/unlock
  unlock: (id: string): Promise<void> => {
    return apiClient.put(`/Users/${id}/unlock`);
  },

  // DELETE: api/Users/{id}
  delete: (id: string): Promise<void> => {
    return apiClient.delete(`/Users/${id}`);
  },

  // PUT: api/Users/{id}/restore
  restore: (id: string): Promise<void> => {
    return apiClient.put(`/Users/${id}/restore`);
  }
};