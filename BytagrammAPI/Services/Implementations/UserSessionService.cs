using BytagrammAPI.Data.Connections.Redis;
using BytagrammAPI.Models.Redis;
using BytagrammAPI.Services.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace BytagrammAPI.Services.Implementations
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IRedisConnection _connection;

        public UserSessionService(IRedisConnection connection)
        {
            _connection = connection;
        }
        private string GetKey(string userId) => $"session:{userId}";

        private async Task<IDatabase> GetDatabase()
        {
            var connection = await _connection.GetConnectionAsync();
            var db = connection.GetDatabase();
            return db;
        }

        public async Task ClearSessionAsync(string userId)
        {
            var db = await GetDatabase();

            await db.KeyDeleteAsync(GetKey(userId));
        }

        public async Task<UserSession?> GetSessionAsync(string userId)
        {
            var db = await GetDatabase();

            var json = await db.StringGetAsync(GetKey(userId));

            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserSession>(json!);
        }

        public async Task SetSessionAsync(UserSession session)
        {
            var db = await GetDatabase();

            var json = JsonSerializer.Serialize(session);

            await db.StringSetAsync(GetKey(session.Id), json, TimeSpan.FromHours(1));
        }
    }
}
