using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class BaseRedisService
    {
        protected readonly IDatabase _db;
        protected readonly string _prefix;

        public BaseRedisService(IConnectionMultiplexer redis, string prefix)
        {
            _db = redis.GetDatabase();
            _prefix = prefix;
        }
    }
}