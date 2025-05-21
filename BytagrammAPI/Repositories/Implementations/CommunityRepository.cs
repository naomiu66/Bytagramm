using BytagrammAPI.Data;
using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;

namespace BytagrammAPI.Repositories.Implementations
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        public CommunityRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
