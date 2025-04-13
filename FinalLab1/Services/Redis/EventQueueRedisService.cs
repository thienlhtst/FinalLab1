using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class EventQueueRedisService : BaseRedisService
    {
        public EventQueueRedisService(IConnectionMultiplexer redis)
            : base(redis, "event_queue_") // Prefix được truyền vào lớp cơ sở
        {
        }

        public async Task<long> EnqueueAsync(int eventId, int userId)
        {
            return await AddToQueueAsync(eventId.ToString(), userId) ? 1 : 0;
        }

        public async Task<long> GetQueueLengthAsync(int eventId)
        {
            return await base.GetQueueLengthAsync(eventId.ToString());
        }

        public async Task<int?> DequeueAsync(int eventId)
        {
            try
            {
                var userId = await _db.ListLeftPopAsync(_prefix + eventId);
                return userId.IsNullOrEmpty ? null : (int?)int.Parse(userId!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error dequeuing user: {ex.Message}");
                return null;
            }
        }

        public async Task RemoveFromQueueAsync(int eventId, int userId)
        {
            await RemoveFromQueueAsync(eventId.ToString(), userId);
        }

        public async Task<List<int>> GetWaitingUsersAsync(int eventId)
        {
            return await GetQueueAsync(eventId.ToString());
        }
    }
}