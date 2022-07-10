
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskContext _context;

        public ProjectRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var project = await _context.Projects.Where(c => c.ProjectId == projectId).FirstOrDefaultAsync();

            return project;

        }
        public async Task<(IEnumerable<Project>, PaginationMetadata)> GetProjectByStatusAsync(bool status, int pageNumber, int pageSize)
        {
            var query = _context.Projects as IQueryable<Project>;

            query = query.Where(p => p.StatusCode == status);

            var totalItemCount = await query.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var queryToReturn = await query.Where(p => p.StatusCode)
                              .Skip(pageSize * pageNumber - 1)
                              .Take(pageSize)
                              .ToListAsync();

            return (queryToReturn, paginationMetaData);
        }

        public async Task<Project>PostProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
   
            return project;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true when one or more items have successfully been changed
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var result = false;
            var entity = _context.Projects.Where(e => e.ProjectId == projectId).FirstOrDefault();

            if (entity != null)
            {
                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

    }
}
