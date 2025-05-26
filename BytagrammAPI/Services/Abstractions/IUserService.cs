using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IUserService : IService<User>
    {
        public Task<IdentityResult> CreateUserAsync(RegisterDto dto, string password);
        public Task<User?> GetByUsernameAsync(string userName);
    }
}