
using Microsoft.EntityFrameworkCore;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TaskContext _context;

        public RoleRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.Where(c => c.RoleId == roleId).FirstOrDefaultAsync();
        }

        public async Task<bool> RoleExistAsync(int roleId)
        {
            if (_context.Roles == null)
            {
                return false;
            }
            return await _context.Roles.AnyAsync(a => a.RoleId == roleId);
        }

        public async Task<Role> PostRolesAsync(Role role)
        {

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }
    }
}
