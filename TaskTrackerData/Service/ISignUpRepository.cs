

using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface ISignUpRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(string? emailAddress,
                         string? searchQuery, int pageNumber, int pageSize);
    }
}