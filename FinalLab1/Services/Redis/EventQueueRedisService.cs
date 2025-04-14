using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class EventQueueRedisService : BaseRedisService
    {
        public EventQueueRedisService(IConnectionMultiplexer redis)
            : base(redis, "event_queue_")
        {
        }

        public async Task<long> EnqueueAsync(int eventId, int userId)
        {
            return await _db.ListRightPushAsync(_prefix + eventId, userId);
        }

        public async Task<long?> GetUserPositionAsync(int eventId, int userId)
        {
            var queue = await _db.ListRangeAsync(_prefix + eventId);
            for (int i = 0; i < queue.Length; i++)
            {
                if (queue[i].ToString() == userId.ToString())
                    return i + 1;
            }
            return null;
        }

        public async Task<int?> DequeueAsync(int eventId)
        {
            var userId = await _db.ListLeftPopAsync(_prefix + eventId);
            return userId.IsNullOrEmpty ? null : (int?)int.Parse(userId!);
        }

        public async Task RemoveFromQueueAsync(int eventId, int userId)
        {
            await _db.ListRemoveAsync(_prefix + eventId, userId, 0);
        }

        public async Task<List<int>> GetWaitingUsersAsync(int eventId)
        {
            var list = await _db.ListRangeAsync(_prefix + eventId);
            return list.Select(item => int.Parse(item.ToString())).ToList();
        }
    }
}