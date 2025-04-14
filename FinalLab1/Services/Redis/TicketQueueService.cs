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
            return await _db.ListLengthAsync(_prefix + seatId);
        }

        public async Task<bool> AddTicketToQueueAsync(int seatId, int userId)
        {
            var ticketCount = await GetSoldCountAsync(seatId);

            var seat = await _genericService.GetByIdAsync(seatId);
            if (seat == null || string.IsNullOrEmpty(seat.CountSeat))
                return false;

            var maxSeat = int.Parse(seat.CountSeat);

            if (ticketCount >= maxSeat)
            {
                return false; // Hết vé
            }

            await _db.ListRightPushAsync(_prefix + seatId, userId);
            return true;
        }

        public async Task<bool> RemoveUserFromQueueAsync(int seatId, int userId)
        {
            var removedCount = await _db.ListRemoveAsync(_prefix + seatId, userId);
            return removedCount > 0;
        }
    }
}