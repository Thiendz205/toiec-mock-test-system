using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Domain.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);

        Task<IEnumerable<User>> GetAllAsync(RecordStatus? status = null, CancellationToken ct = default);
        Task<IEnumerable<User>> GetAllActiveUsersAsync(CancellationToken ct = default);

        Task AddAsync(User user, CancellationToken ct = default);
        void Update(User user);
        void Delete(User user);
    }
}
