using StackExchange.Redis;

namespace BytagrammAPI.Data.Connections.Redis
{
    public interface IRedisConnection
    {
        Task<IConnectionMultiplexer> GetConnectionAsync();
    }
}
