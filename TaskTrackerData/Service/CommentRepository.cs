
using Microsoft.EntityFrameworkCore;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskContext _context;

        public CommentRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentsByIdAsync(int commentId)
        {

            return _context.Comments.Where(c => c.commentId == commentId).FirstOrDefault();

        }

        public async Task<(IEnumerable<Comment>, PaginationMetadata)> GetCommentAsync(string? query,
                                                                int pageNumber, int pageSize)
        {

            var comments = _context.Comments as IQueryable<Comment>;

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.Trim();
                comments = comments.Where(a => a.Title.Contains(query)
                                            || a.Body.Contains(query));
            }


            var totalItemCount = await comments.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var commentsToreturn = await comments.OrderBy(c => c.Title)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToListAsync();

            return (commentsToreturn, paginationMetadata);
        }

        public async Task<Comment> PostProjectAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true when one or more items have successfully been changed
            return await _context.SaveChangesAsync() >= 0;
        }
        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var result = false;
            var entity = _context.Comments.Where(e => e.commentId == commentId).FirstOrDefault();

            if (entity != null)
            {
                _context.Comments.Remove(entity);
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }




    }
}
