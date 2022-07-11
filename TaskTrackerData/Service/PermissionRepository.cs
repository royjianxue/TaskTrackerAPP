
using Microsoft.EntityFrameworkCore;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly TaskContext _context;

        public PermissionRepository(TaskContext context)
        {
            _context = context;
        }
        public async Task<Permission> GetPermissionByIdAsync(int permissionId)
        {
            return await _context.Permissions.Where(c => c.PermissionId == permissionId).FirstOrDefaultAsync();
        }

        public async Task<bool> PermissionExistAsync(int permissionIdeId)
        {
            if (_context.Permissions == null)
            {
                return false;
            }
            return await _context.Permissions.AnyAsync(a => a.PermissionId == permissionIdeId);
        }

        public async Task<Permission> PostPermissionAsync(Permission permission)
        {

            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();

            return permission;
        }
    }
}
