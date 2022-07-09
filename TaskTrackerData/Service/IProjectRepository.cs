using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface IProjectRepository
    {
        Task<(IEnumerable<Project>, PaginationMetadata)> GetProjectByStatusAsync(bool status, int pageNumber, int pageSize);

        Task<bool> ProjectExistAsync(int projectId);

        Task<Project> GetProjectByIdAsync(int projectId);

        Task<Project> PostProjectAsync(Project project);

        Task<bool> SaveChangesAsync();
    }
}