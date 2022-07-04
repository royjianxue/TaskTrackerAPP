using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface ISignUpRepository
    {
        Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(string? emailAddress, string? searchQuery, int pageNumber, int pageSize);
        Task<User> PostUserAsync(User user);
        Task<bool> SaveChangesAsync();
        Task<bool> UserExistAsync(int userId);
    }
}