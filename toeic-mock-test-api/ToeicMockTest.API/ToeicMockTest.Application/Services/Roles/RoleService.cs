using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Roles.Requests;
using ToeicMockTest.Contracts.Roles.Responses;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.Domain.Repositories;
using ToeicMockTest.Domain.Repositories.Roles;

namespace ToeicMockTest.Application.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken ct = default)
        {
            // Lấy toàn bộ danh sách quyền
            var roles = await _roleRepository.GetAllAsync(ct);

            return roles.Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsSystem = r.IsSystem,
                UserCount = r.Users.Count
            });
        }

        public async Task<RoleResponse> CreateAsync(CreateRoleRequest request, CancellationToken ct = default)
        {
            // Kiểm tra tên quyền đã tồn tại hay chưa
            var existing = await _roleRepository.GetByNameAsync(request.Name, ct);
            if (existing != null)
            {
                throw new Exception("Role name already exists.");
            }

            var role = new Role(request.Name, request.Description);

            // Thêm và lưu vào database
            await _roleRepository.AddAsync(role, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new RoleResponse { Id = role.Id, Name = role.Name, Description = role.Description };
        }

        public async Task UpdateAsync(Guid id, UpdateRoleRequest request, CancellationToken ct = default)
        {
            // Tìm quyền cần cập nhật
            var role = await _roleRepository.GetByIdAsync(id, ct);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            role.Update(request.Name, request.Description);

            // Đánh dấu cập nhật và lưu
            _roleRepository.Update(role);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            // Tìm quyền cần xóa
            var role = await _roleRepository.GetByIdAsync(id, ct);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            // Không cho phép xóa quyền hệ thống
            if (role.IsSystem)
            {
                throw new Exception("Cannot delete system role.");
            }

            // Kiểm tra nếu có người dùng đang giữ quyền này
            bool hasUsers = await _roleRepository.HasUserAsync(id, ct);
            if (hasUsers)
            {
                throw new Exception("Cannot delete role because it is assigned to users.");
            }

            // Thực hiện xóa và lưu thay đổi
            _roleRepository.Delete(role);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}