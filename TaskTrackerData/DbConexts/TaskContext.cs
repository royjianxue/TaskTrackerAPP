using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTrackerData.Domain;

namespace TaskTrackerData.DbConexts
{
    public class TaskContext : DbContext
    {
        public DbSet<Project>? Projects { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<User>? Users { get; set; }

        public TaskContext(DbContextOptions options) : base(options)
        {
            // override constructor on program.cs
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }




    }
}
