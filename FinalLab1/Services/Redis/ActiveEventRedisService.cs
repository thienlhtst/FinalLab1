using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class ActiveEventRedisService : BaseRedisService
    {
        private readonly string _activePrefix = "active_users:";

        public ActiveEventRedisService(IConnectionMultiplexer redis)
            : base(redis, "") // Prefix được truyền vào lớp cơ sở
        {
        }

        public async Task<long> EnqueueAsync(int eventId, int userId)
        {
            return await AddToQueueAsync(eventId.ToString(), userId) ? 1 : 0;
        }

        public async Task<long?> GetUserPositionAsync(int eventId, int userId)
        {
            var queue = await GetQueueAsync(eventId.ToString());
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i] == userId)
                    return i + 1;
            }
            return null;
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

        public async Task<bool> AddActiveUserAsync(int eventId, int userId)
        {
            try
            {
                return await _db.SetAddAsync(_activePrefix + eventId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding active user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveActiveUserAsync(int eventId, int userId)
        {
            try
            {
                return await _db.SetRemoveAsync(_activePrefix + eventId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing active user: {ex.Message}");
                return false;
            }
        }

        public async Task<List<int>> GetActiveUsersAsync(int eventId)
        {
            try
            {
                var members = await _db.SetMembersAsync($"{_activePrefix}{eventId}");
                return members.Select(m => int.Parse(m.ToString())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active users: {ex.Message}");
                return new List<int>();
            }
        }

        public async Task<List<int>> GetWaitingUsersAsync(int eventId)
        {
            return await GetQueueAsync(eventId.ToString());
        }

        public async Task<long> GetActiveUserCountAsync(int eventId)
        {
            try
            {
                return await _db.SetLengthAsync(_activePrefix + eventId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active user count: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> IsUserActiveAsync(int eventId, int userId)
        {
            try
            {
                return await _db.SetContainsAsync(_activePrefix + eventId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if user is active: {ex.Message}");
                return false;
            }
        }
    }
}