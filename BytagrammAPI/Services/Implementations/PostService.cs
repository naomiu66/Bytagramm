using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;

namespace BytagrammAPI.Services.Implementations
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository repository) : base(repository)
        {
            _postRepository = repository;
        }
    }
}
