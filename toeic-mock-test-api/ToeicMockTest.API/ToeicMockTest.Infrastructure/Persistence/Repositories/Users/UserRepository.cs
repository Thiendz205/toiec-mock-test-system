using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.SharedKernel.Common.Enums;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.Domain.Repositories.Users;

namespace ToeicMockTest.Infrastructure.Persistence.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id , ct); // Truyền ct vào đây
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Status != RecordStatus.Delete, ct);
        }

        public async Task<User?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Name == name && u.Status != RecordStatus.Delete, ct); 
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Status != RecordStatus.Delete, ct); 
        }

        public async Task<IEnumerable<User>> GetAllAsync(RecordStatus? status = null, CancellationToken ct = default)
        {
            var query = _context.Users.Include(u => u.Role).AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(u => u.Status == status.Value);
            }
            else
            {
                query = query.Where(u => u.Status != RecordStatus.Delete);
            }

            return await query.ToListAsync(ct);
        }

        public async Task<IEnumerable<User>> GetAllActiveUsersAsync(CancellationToken ct = default)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Status == RecordStatus.Active)
                .ToListAsync(ct);
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _context.Users.AddAsync(user, ct);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            user.SoftDelete();
            _context.Users.Update(user);
        }
    }
}
