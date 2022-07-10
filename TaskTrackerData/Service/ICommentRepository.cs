using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public interface ICommentRepository
    {
        Task<(IEnumerable<Comment>, PaginationMetadata)> GetCommentAsync(string? query, int pageNumber, int pageSize);
        Task<Comment> GetCommentsByIdAsync(int commentId);
        Task<Comment> PostProjectAsync(Comment comment);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteCommentAsync(int commentId);

    }
}