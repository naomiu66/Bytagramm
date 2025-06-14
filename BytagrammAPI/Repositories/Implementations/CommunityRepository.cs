using BytagrammAPI.Data;
using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BytagrammAPI.Repositories.Implementations
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        public CommunityRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<Community> GetByIdAsync(string id)
        {
            return await GetAllAsQueryable()
                .Include(c => c.Posts)
                .Include(c => c.Subscribers)
                .Include(c => c.Author)
                .FirstAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Community>> GetAllAsync()
        {
            return await GetAllAsQueryable()
                .Include(c => c.Posts)
                .Include(c => c.Subscribers)
                .Include(c => c.Author)
                .ToListAsync();
        }
    }
}
