using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class TicketQueueService
    {
        private readonly IDatabase _db;
        private readonly IGenericService<Seat, int> _enericService;
        private readonly string _prefix = "event_queue_";

        public TicketQueueService(IConnectionMultiplexer redis, IGenericService<Seat, int> genericService)
        {
            _db = redis.GetDatabase();
            _enericService = genericService;
        }

        public async Task<long> GetSoldCountAsync(int seatId)
        {
            return await _db.ListLengthAsync(_prefix + seatId); // Lấy số lượng vé đã bán
        }

        public async Task<bool> AddTicketToQueueAsync(int seatId, int userId)
        {
            var ticketCount = await GetSoldCountAsync(seatId);

            // Kiểm tra số ghế còn lại
            var seat = await _enericService.GetByIdAsync(seatId);
            var maxSeat = int.Parse(seat.CountSeat);

            if (ticketCount >= maxSeat)
            {
                return false; // Vé đã hết
            }

            await _db.ListRightPushAsync(_prefix + seatId, userId); // Thêm vào queue Redis
            return true;
        }

        public async Task<bool> RemoveUserFromQueueAsync(int seatId, int userId)
        {
            return await _db.ListRemoveAsync(_prefix + seatId, userId) > 0; // Gỡ người dùng khỏi queue Redis
        }
    }
}