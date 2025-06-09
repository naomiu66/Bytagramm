using BytagrammAPI.Models.Redis;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IUserSessionService
    {
        public Task<UserSession?> GetSessionAsync(string userId);
        public Task SetSessionAsync(UserSession session);
        public Task ClearSessionAsync(string userId);
    }
}
