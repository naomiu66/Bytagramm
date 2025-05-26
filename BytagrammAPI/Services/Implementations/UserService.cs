using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Services.Implementations
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager) : base(userRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto dto, string password)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<User?> GetByUsernameAsync(string userName)
        {
            return await _userRepository.GetByUsernameAsync(userName);
        }
    }
}