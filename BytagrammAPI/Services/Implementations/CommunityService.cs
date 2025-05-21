using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;

namespace BytagrammAPI.Services.Implementations
{
    public class CommunityService : Service<Community>, ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;
        public CommunityService(ICommunityRepository repository) : base(repository)
        {
            _communityRepository = repository;
        }
    }
}
