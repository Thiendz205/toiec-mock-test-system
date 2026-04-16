using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.Domain.Repositories.Roles;

namespace ToeicMockTest.Infrastructure.Persistence.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Users)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public void Update(Role role)
        {
            _context.Update(role);
        }

        public async Task<bool> HasUserAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.RoleId == roleId, cancellationToken);
        }
    }
}
