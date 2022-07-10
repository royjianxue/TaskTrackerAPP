using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface IProjectRepository
    {
        Task<(IEnumerable<Project>, PaginationMetadata)> GetProjectByStatusAsync(bool? status, int pageNumber, int pageSize);

        Task<Project> GetProjectByIdAsync(int projectId);

        Task<Project> PostProjectAsync(Project project);

        Task<bool> ProjectExistAsync(int projectId);

        Task<bool> SaveChangesAsync();

        Task<bool> DeleteProjectAsync(int projectId);
    }
}