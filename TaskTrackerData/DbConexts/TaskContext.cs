using Common.Contract.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimeStamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var now = DateTime.Now;

            var entries = ChangeTracker.Entries()
                    .Where(e => e.Entity is DateTimeDto &&
                            (e.State == EntityState.Added
                             || e.State == EntityState.Modified));

            foreach (var entityEntity in entries)
            {
                ((DateTimeDto)entityEntity.Entity).UpdatedDate = now;
                if (entityEntity.State == EntityState.Added)
                {
                    ((DateTimeDto)entityEntity.Entity).CreatedDate = now;
                }
            }
        }
    }
}
