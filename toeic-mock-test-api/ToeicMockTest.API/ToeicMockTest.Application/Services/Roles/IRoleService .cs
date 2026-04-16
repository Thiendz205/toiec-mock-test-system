using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Roles.Requests;
using ToeicMockTest.Contracts.Roles.Responses;

namespace ToeicMockTest.Application.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken ct = default);
        Task<RoleResponse> CreateAsync(CreateRoleRequest request, CancellationToken ct = default);
        Task UpdateAsync(Guid id, UpdateRoleRequest role, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
