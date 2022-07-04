using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
            Permissions = new List<Permission>();
        }
        public int RoleId { get; set; }

        public string? Name { get; set; }
        public List<User> Users { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
