using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface IPermissionRepository
    {
        Task<Permission> GetPermissionByIdAsync(int permissionId);
        Task<bool> PermissionExistAsync(int permissionIdeId);
        Task<Permission> PostPermissionAsync(Permission permission);
    }
}