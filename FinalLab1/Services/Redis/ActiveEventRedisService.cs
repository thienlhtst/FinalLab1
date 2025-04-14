using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class ActiveEventRedisService : BaseRedisService
    {
        public ActiveEventRedisService(IConnectionMultiplexer redis)
            : base(redis, "active_users:")
        {
        }

        public async Task<bool> AddActiveUserAsync(int eventId, int userId)
        {
            return await _db.SetAddAsync(_prefix + eventId, userId);
        }

        public async Task<bool> RemoveActiveUserAsync(int eventId, int userId)
        {
            return await _db.SetRemoveAsync(_prefix + eventId, userId);
        }

        public async Task<List<int>> GetActiveUsersAsync(int eventId)
        {
            var members = await _db.SetMembersAsync(_prefix + eventId);
            return members.Select(m => int.Parse(m.ToString())).ToList();
        }

        public async Task<long> GetActiveUserCountAsync(int eventId)
        {
            return await _db.SetLengthAsync(_prefix + eventId);
        }

        public async Task<bool> IsUserActiveAsync(int eventId, int userId)
        {
            return await _db.SetContainsAsync(_prefix + eventId, userId);
        }
    }
}