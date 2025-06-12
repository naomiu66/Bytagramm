using BytagrammAPI.Data.Connections.Redis;
using BytagrammAPI.Models.Redis;
using BytagrammAPI.Services.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace BytagrammAPI.Services.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IRedisConnection _redisConnection;
        
        public CacheService(IRedisConnection redisConnection) 
        {
            _redisConnection = redisConnection;
        }

        private async Task<IDatabase> GetDatabase()
        {
            var connection = await _redisConnection.GetConnectionAsync();
            var db = connection.GetDatabase();
            return db;
        }

        public async Task RemoveAsync(string key)
        {
            var db = await GetDatabase();

            await db.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync<T>(string key) where T : class, ICachable
        {
            var db = await GetDatabase();

            var json = await db.StringGetAsync(key);

            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(json!);
        }

        public async Task SetAsync<T>(Cache<T> cache) where T : class, ICachable
        {
            var db = await GetDatabase();

            var json = JsonSerializer.Serialize(cache.Payload);

            await db.StringSetAsync(cache.Key, json, TimeSpan.FromHours(0.25));
        }
    }
}
