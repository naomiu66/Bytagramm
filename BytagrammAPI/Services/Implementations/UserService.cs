using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;

namespace BytagrammAPI.Services.Implementations
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}