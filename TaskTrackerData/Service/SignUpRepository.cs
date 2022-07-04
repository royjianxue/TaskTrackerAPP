using Microsoft.EntityFrameworkCore;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Domain;

namespace TaskTrackerData.Service
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly TaskContext _context;

        public SignUpRepository(TaskContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(c => c.EmailAddress).ToListAsync();
        }
        public async Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(string? emailAddress, 
                         string? searchQuery, int pageNumber, int pageSize)
        {
            //collection to start from
            // query is only sent to the database at the end
            // when return ToListAsync() is reached
            var collection = _context.Users as IQueryable<User>;

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                emailAddress = emailAddress.Trim();
                collection = collection.Where(c => c.EmailAddress == emailAddress);
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.EmailAddress.Contains(searchQuery)
                          || (a.UserName != null && a.UserName.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToreturn = await collection.OrderBy(c => c.EmailAddress)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToListAsync();

            return (collectionToreturn, paginationMetadata);
        }

    }
}
