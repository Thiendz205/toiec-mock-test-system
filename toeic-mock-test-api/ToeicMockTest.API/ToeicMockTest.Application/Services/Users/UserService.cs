using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Users.Requests;
using ToeicMockTest.Contracts.Users.Responses;
using ToeicMockTest.Domain.Common.Constants;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.Domain.Repositories;
using ToeicMockTest.Domain.Repositories.Users;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // Tạo người dùng mới (Dành cho Admin)
        public async Task<UserResponse> CreateUserAsync(CreateUserRequest request, Guid adminId, CancellationToken ct = default)
        {
            // 1. Kiểm tra trùng Email
            if (await _userRepository.ExistsByEmailAsync(request.Email, ct))
            {
                throw new Exception("Email already exists in the system.");
            }

            // 2. Kiểm tra trùng Username
            var existingName = await _userRepository.GetByNameAsync(request.Name, ct);
            if (existingName != null)
            {
                throw new Exception("Username already exists.");
            }

            // Giả sử mật khẩu đã được xử lý trước đó hoặc tại đây
            string hashedPassword = request.Password;

            var user = new User(
                request.Name,
                request.Email,
                hashedPassword,
                request.FirstName,
                request.LastName,
                request.RoleId
            );

            // Lưu người dùng vào database
            await _userRepository.AddAsync(user, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                Status = user.Status,
                CreatedDate = user.CreatedDate
            };
        }

        // Lấy danh sách toàn bộ người dùng
        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(RecordStatus? status = null, CancellationToken ct = default)
        {
            // Truyền status xuống Repository
            var users = await _userRepository.GetAllAsync(status, ct);

            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RoleId = u.RoleId,
                RoleName = u.Role?.Name ?? "No Role",
                Status = u.Status,
                CreatedDate = u.CreatedDate
            });
        }

        // Lấy thông tin chi tiết người dùng theo ID
        public async Task<UserDetailResponse?> GetUserByIdAsync(Guid userId, CancellationToken ct = default)
        {
            var u = await _userRepository.GetByIdAsync(userId, ct);
            if (u == null)
            {
                return null;
            }

            return new UserDetailResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RoleId = u.RoleId,
                RoleName = u.Role?.Name ?? "No Role",
                Status = u.Status,
                CreatedDate = u.CreatedDate,
                CreatedById = u.CreatedById,
                UpdatedDate = u.UpdatedDate,
                UpdatedById = u.UpdatedById
            };
        }

        // Admin cập nhật thông tin người dùng
        public async Task<bool> AdminUpdateUserAsync(UpdateUserRequest request, Guid adminId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, ct);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Kiểm tra nếu đổi email thì email mới không được trùng
            if (user.Email != request.Email && await _userRepository.ExistsByEmailAsync(request.Email, ct))
            {
                throw new Exception("The new email is already in use.");
            }

            user.AdminUpdateUser(
                request.FirstName,
                request.LastName,
                request.Email,
                request.RoleId,
                adminId
            );

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }

        // Người dùng tự cập nhật Profile
        public async Task<bool> UpdateProfileAsync(Guid userId, UpdateProfileRequest request, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.UpdateProfile(request.FirstName, request.LastName, userId);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> LockUserAsync(Guid userId, Guid adminId, CancellationToken ct = default)
        {
            // CHẶN: Admin không được tự khóa chính mình
            if (userId == adminId)
            {
                throw new Exception("You cannot lock your own account.");
            }

            var user = await _userRepository.GetByIdAsync(userId, ct);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // CHẶN: Không cho phép khóa tài khoản Admin hệ thống (dựa vào RoleId cố định)
            if (user.RoleId == RoleConstants.AdminId)
            {
                throw new Exception("Cannot lock an Admin account to prevent system lockout.");
            }

            // Chuyển trạng thái sang Suspended (2)
            user.Suspend(); // Giả sử hàm này gán Status = RecordStatus.Suspended
            user.SetUpdatedInfo(adminId);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }

        // 2. Logic MỞ KHÓA tài khoản
        public async Task<bool> UnlockUserAsync(Guid userId, Guid adminId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Chuyển trạng thái quay lại Active (1)
            user.UnSuspend(); // Giả sử hàm này gán Status = RecordStatus.Active
            user.SetUpdatedInfo(adminId);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
        // Xóa người dùng (Soft Delete)
        public async Task<bool> DeleteUserAsync(Guid userId, Guid adminId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.SoftDelete();
            user.SetUpdatedInfo(adminId);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
        public async Task<bool> RestoreUserAsync(Guid userId, Guid adminId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Gọi hàm Restore trong Entity bạn đã viết
            user.Restore();
            user.SetUpdatedInfo(adminId);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}
