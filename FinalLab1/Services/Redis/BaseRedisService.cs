using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class BaseRedisService
    {
        protected readonly IDatabase _db;
        protected readonly string _prefix;

        protected BaseRedisService(IConnectionMultiplexer redis, string prefix)
        {
            _db = redis.GetDatabase();
            _prefix = prefix;
        }

        // Thêm một phần tử vào danh sách (queue)
        public async Task<bool> AddToQueueAsync(string key, int value)
        {
            try
            {
                await _db.ListRightPushAsync(_prefix + key, value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding to queue: {ex.Message}");
                return false;
            }
        }

        // Lấy độ dài của danh sách (queue)
        public async Task<long> GetQueueLengthAsync(string key)
        {
            try
            {
                return await _db.ListLengthAsync(_prefix + key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting queue length: {ex.Message}");
                return 0;
            }
        }

        // Xóa một phần tử khỏi danh sách (queue)
        public async Task<bool> RemoveFromQueueAsync(string key, int value)
        {
            try
            {
                return await _db.ListRemoveAsync(_prefix + key, value) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing from queue: {ex.Message}");
                return false;
            }
        }

        // Lấy toàn bộ danh sách (queue)
        public async Task<List<int>> GetQueueAsync(string key)
        {
            try
            {
                var list = await _db.ListRangeAsync(_prefix + key);
                return list.Select(item => int.Parse(item.ToString())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting queue: {ex.Message}");
                return new List<int>();
            }
        }

        // Kiểm tra xem một phần tử có tồn tại trong danh sách (queue) hay không
        public async Task<bool> IsInQueueAsync(string key, int value)
        {
            try
            {
                var queue = await _db.ListRangeAsync(_prefix + key);
                return queue.Any(item => item.ToString() == value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking queue: {ex.Message}");
                return false;
            }
        }
    }
}