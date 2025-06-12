using BytagrammAPI.Models.Redis;

namespace BytagrammAPI.Services.Abstractions
{
    public interface ICacheService
    {
        public Task<T> GetAsync<T>(string key) where T : class, ICachable;
        public Task SetAsync<T>(Cache<T> cache) where T : class, ICachable;
        public Task RemoveAsync(string key);
    }
}
