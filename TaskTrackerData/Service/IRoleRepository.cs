using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<Role> PostRolesAsync(Role role);
        Task<bool> RoleExistAsync(int roleId);
    }
}