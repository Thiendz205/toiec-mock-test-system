using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Users.Requests;
using ToeicMockTest.Contracts.Users.Responses;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Application.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(CreateUserRequest request, Guid adminId, CancellationToken ct = default);
        Task<bool> AdminUpdateUserAsync(UpdateUserRequest request, Guid adminId, CancellationToken ct = default);
        Task<bool> UpdateProfileAsync(Guid userId, UpdateProfileRequest request, CancellationToken ct = default);
        Task<UserDetailResponse?> GetUserByIdAsync(Guid userId, CancellationToken ct = default);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync(RecordStatus? status = null, CancellationToken ct = default);
        Task<bool> LockUserAsync(Guid userId, Guid adminId, CancellationToken ct = default);
        Task<bool> UnlockUserAsync(Guid userId, Guid adminId, CancellationToken ct = default);
        Task<bool> DeleteUserAsync(Guid userId, Guid adminId, CancellationToken ct = default);
        Task<bool> RestoreUserAsync(Guid userId, Guid adminId, CancellationToken ct = default);
    }
}
