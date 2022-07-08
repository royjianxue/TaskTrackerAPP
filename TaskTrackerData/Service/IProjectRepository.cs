using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface IProjectRepository
    {
        Task<(IEnumerable<Project>, PaginationMetadata)> GetProjectByStatus(bool status, string? searchQuery, int pageNumber, int pageSize);
    }
}