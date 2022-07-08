using Common.Contract.Model;

namespace TaskTrackerData.Domain
{
    public class User : DateTimeDto
    {
        public User()
        {
            Roles = new List<Role>();
            Projects = new List<Project>();
            Comments = new List<Comment>();
        }
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? EmailAddress { get; set; }
 
        public string? Password { get; set; }
        public List<Role> Roles { get; set; }
        public List<Project> Projects { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
