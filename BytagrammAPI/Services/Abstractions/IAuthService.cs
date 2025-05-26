using BytagrammAPI.Dto;
using BytagrammAPI.Models;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}
