using BytagrammAPI.Dto.User;
using BytagrammAPI.Models;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}
