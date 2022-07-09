﻿
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

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var newUser = await _context.Projects.Where(c => c.ProjectId == projectId).FirstOrDefaultAsync();

            return newUser;

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


        public async Task<bool> ProjectExistAsync(int projectId)
        {
            if (_context.Projects == null)
            {
                return false;
            }
            return await _context.Projects.AnyAsync(p => p.ProjectId == projectId);
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


    }
}
