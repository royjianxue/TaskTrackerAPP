using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Permission
    {
        public Permission()
        {
            Roles = new List<Role>();
        }
        public int PermissionId { get; set; }

        public string? Type { get; set; }
        public List<Role> Roles { get; set; }
    }
}
