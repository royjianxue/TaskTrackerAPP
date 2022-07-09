using Common.Contract.Model;

namespace TaskTrackerData.Domain
{
    public class Permission : DateTimeDto
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
