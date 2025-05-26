using BytagrammAPI.Dto;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserDto?> LoginAsync(LoginDto loginDto);
    }
}
