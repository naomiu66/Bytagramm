using BytagrammAPI.Data;
using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BytagrammAPI.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await GetAllAsQueryable()
                .Include(p => p.Author)
                .ToListAsync();
        }

        public override async Task<Post> GetByIdAsync(string id)
        {
            return await GetAllAsQueryable()
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
