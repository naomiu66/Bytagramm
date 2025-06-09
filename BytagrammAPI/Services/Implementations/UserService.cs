using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Services.Implementations
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, UserManager<User> userManager) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<User> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetByRefreshTokenAsync(refreshToken);
        }

        public async Task<User?> GetByUsernameAsync(string userName)
        {
            return await _userRepository.GetByUsernameAsync(userName);
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            return await _userRepository.VerifyPasswordAsync(user, password);
        }
    }
}