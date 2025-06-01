using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IUserService : IService<User>
    {
        public Task<User> GetByRefreshTokenAsync(string refreshToken);
        public Task<User?> GetByUsernameAsync(string userName);
        public Task<User?> GetByEmailAsync(string email);
        public Task<bool> VerifyPasswordAsync(User user, string password);
    }
}