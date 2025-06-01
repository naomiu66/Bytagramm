

using Bytagramm.Dto;

namespace Bytagramm.Services.Abstractions
{
    public interface IUserApiService
    {
        public Task<List<UserDto>?> GetAllAsync();
        public Task<UserDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(RegisterDto user);
        public Task<bool> UpdateAsync(string id, UserDto user);
        public Task<bool> DeleteAsync(string id);
        public Task<bool> AuthenticateAsync(string usernameOrEmail, string password);
        public Task LogoutAsync();
        public Task<bool> InitializeAsync();

    }
}
