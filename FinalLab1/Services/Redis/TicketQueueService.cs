using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace FinalLab1.Services.Redis
{
    public class TicketQueueService : BaseRedisService
    {
        private readonly IGenericService<Seat, int> _genericService;

        public TicketQueueService(IConnectionMultiplexer redis, IGenericService<Seat, int> genericService)
            : base(redis, "ticket_queue_") // Prefix được truyền vào lớp cơ sở
        {
            _genericService = genericService;
        }

        public async Task<long> GetSoldCountAsync(int seatId)
        {
            return await GetQueueLengthAsync(seatId.ToString()); // Lấy số lượng vé đã bán
        }

        public async Task<bool> AddTicketToQueueAsync(int seatId, int userId)
        {
            var ticketCount = await GetSoldCountAsync(seatId);

            // Kiểm tra số ghế còn lại
            var seat = await _genericService.GetByIdAsync(seatId);
            var maxSeat = int.Parse(seat.CountSeat);

            if (ticketCount >= maxSeat)
            {
                return false; // Vé đã hết
            }

            return await AddToQueueAsync(seatId.ToString(), userId); // Thêm vào queue Redis
        }

        public async Task<bool> RemoveUserFromQueueAsync(int seatId, int userId)
        {
            return await RemoveFromQueueAsync(seatId.ToString(), userId); // Gỡ người dùng khỏi queue Redis
        }
    }
}