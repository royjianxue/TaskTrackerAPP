
using Microsoft.EntityFrameworkCore;
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

        public async Task<(IEnumerable<Project>, PaginationMetadata)> GetProjectByStatus(bool status, string? searchQuery, int pageNumber, int pageSize)
        {

            var query = _context.Projects as IQueryable<Project>;


            query = query.Where(p => p.StatusCode == status);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(p => p.StatusCode == status && p.Title != null && p.Title.Contains(searchQuery));
            }

            var totalItemCount = await query.CountAsync();
            var pagination = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var queryToReturn = await query.Where(p => p.StatusCode)
                              .Skip(pageSize * pageNumber - 1)
                              .Take(pageSize)
                              .ToListAsync();

            return (queryToReturn, pagination);
        }


    }
}
