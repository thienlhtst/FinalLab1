using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class RedisQueueService
    {
        private readonly IDatabase _db;
        private readonly string _prefix = "event_queue_";
        private readonly string _activePrefix = "active_users:";

        public RedisQueueService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
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

        public async Task<long> GetQueueLengthAsync(int eventId)
        {
            return await _db.ListLengthAsync(_prefix + eventId);
        }

        public async Task<int?> DequeueAsync(int eventId)
        {
            var userId = await _db.ListLeftPopAsync(_prefix + eventId);
            return userId.IsNullOrEmpty ? null : (int?)int.Parse(userId!);
        }

        public async Task RemoveFromQueueAsync(int eventId, int userId)
        {
            // Xóa tất cả các lần xuất hiện userId trong hàng đợi
            await _db.ListRemoveAsync(_prefix + eventId, userId, 0); // 0 nghĩa là xóa tất cả
        }

        /// <summary>
        /// ////////////////////////////////////
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddActiveUserAsync(int eventId, int userId)
        {
            return await _db.SetAddAsync(_activePrefix + eventId, userId);
        }

        public async Task<bool> RemoveActiveUserAsync(int eventId, int userId)
        {
            return await _db.SetRemoveAsync(_activePrefix + eventId, userId);
        }

        public async Task<List<int>> GetActiveUsersAsync(int eventId)
        {
            var members = await _db.SetMembersAsync($"{_activePrefix}{eventId}");
            return members.Select(m => int.Parse(m.ToString())).ToList();
        }

        public async Task<List<int>> GetWaitingUsersAsync(int eventId)
        {
            var list = await _db.ListRangeAsync(_prefix + eventId);
            return list.Select(item => int.Parse(item.ToString())).ToList();
        }

        public async Task<long> GetActiveUserCountAsync(int eventId)
        {
            return await _db.SetLengthAsync(_activePrefix + eventId);
        }

        public async Task<bool> IsUserActiveAsync(int eventId, int userId)
        {
            return await _db.SetContainsAsync(_activePrefix + eventId, userId);
        }
    }
}