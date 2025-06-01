using BytagrammAPI.Models;

namespace BytagrammAPI.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByRefreshTokenAsync(string refreshToken);
        public Task<bool> VerifyPasswordAsync(User user, string password);
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetByEmailAsync(string email);
    }
}