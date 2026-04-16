import { useQuery,useMutation, useQueryClient } from '@tanstack/react-query';
import { userService } from  '../services/user.service';  
import { RecordStatusType } from '@/types/common.type';
import { CreateUserRequest, UpdateUserRequest } from '../types';

export const useUser = () => {
  const queryClient = useQueryClient();


  // 1. Lấy danh sách users
  const useGetUsers = (status?: RecordStatusType) => {
    return useQuery({
      queryKey: ['users', status],
      queryFn: () => userService.getAll(status),
    });
  };

  // 2. Lấy chi tiết 1 user
  const useGetDetail = (id: string) => {
    return useQuery({
      queryKey: ['users', id],
      queryFn: () => userService.getById(id),
      enabled: !!id, 
    });
  };

  // 3. Tạo mới user
  const createMutation = useMutation({
    mutationFn: (data: CreateUserRequest) => userService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // 4. Admin cập nhật user
  const updateMutation = useMutation({
    mutationFn: (data: UpdateUserRequest) => userService.adminUpdate(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // 5. Khóa user
  const lockMutation = useMutation({
    mutationFn: (id: string) => userService.lock(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // 6. Mở khóa user
  const unlockMutation = useMutation({
    mutationFn: (id: string) => userService.unlock(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // 7. Xóa user
  const deleteMutation = useMutation({
    mutationFn: (id: string) => userService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // 8. Khôi phục user
  const restoreMutation = useMutation({
    mutationFn: (id: string) => userService.restore(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  return {
    useGetUsers,
    useGetDetail,
    createMutation,
    updateMutation,
    lockMutation,
    unlockMutation,
    deleteMutation,
    restoreMutation,
  };
};