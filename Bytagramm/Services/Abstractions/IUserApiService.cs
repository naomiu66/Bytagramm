using Bytagramm.Models.User;

namespace Bytagramm.Services.Abstractions
{
    public interface IUserApiService
    {
        public Task<List<UserDto>?> GetAllAsync();
        public Task<UserDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(RegisterDto user);
        public Task<ProfileDto?> GetCurrentUserAsync();
        public Task<bool> UpdateAsync(string id, UserDto user);
        public Task<bool> DeleteAsync(string id);
        public Task<bool> LoginAsync(LoginDto dto);
    }
}
