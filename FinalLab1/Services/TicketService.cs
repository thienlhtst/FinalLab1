using FinalLab1.Data;
using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLab1.Services
{
    public class TicketService
    {
        private readonly LabDbContext _dbContext;

        public TicketService(LabDbContext context)
        {
            _dbContext=context;
        }

        public async Task<string> BookTicketAsync(TicketRequest data)
        {
            // Lấy ghế từ DB
            var seat = await _dbContext.Seats
                .Include(s => s.DetailSeats)
                .ThenInclude(ds => ds.NumberSeats)
                .FirstOrDefaultAsync(s => s.Id == data.SeatId);

            if (seat == null)
            {
                return "Ghế không tồn tại";
            }

            // Nếu ghế có DetailSeat
            if (seat.DetailSeats.Any())
            {
                var detailSeat = seat.DetailSeats.FirstOrDefault(ds => ds.Id == data.DetailSeatId);
                if (detailSeat == null)
                {
                    return "Không tìm thấy DetailSeat";
                }

                var numberSeat = detailSeat.NumberSeats.FirstOrDefault(ns => ns.Id == data.NumberSeatId);
                if (numberSeat == null)
                {
                    return "Không tìm thấy NumberSeat";
                }

                // Kiểm tra số lượng vé đã bán cho SeatId và NumberSeatId
                var soldCount = await _dbContext.Tickets
                    .Where(t => t.SeatId == data.SeatId && t.NumberSeatId == data.NumberSeatId)
                    .CountAsync();

                var maxSeat = int.Parse(seat.CountSeat); // Tổng số ghế của Seat

                if (soldCount >= maxSeat)
                {
                    return "Vé đã hết";
                }

                // Tạo ticket cho người dùng
                var ticket = new Ticket
                {
                    UserId = data.UserId,
                    SeatId = data.SeatId,
                    NumberSeatId= data.NumberSeatId,
                    Status = TicketStatus.Pending,
                    TypeTicket = TicketType.Standard,
                    QrImage = "", // Gọi hàm generate QR code
                    Date = DateTimeOffset.Now
                };

                _dbContext.Tickets.Add(ticket);
                await _dbContext.SaveChangesAsync();

                return "Đặt vé thành công";
            }
            else
            {
                // Nếu ghế không có DetailSeat, chỉ cần kiểm tra SeatId
                var soldCount = await _dbContext.Tickets
                    .Where(t => t.SeatId == data.SeatId)
                    .CountAsync();

                var maxSeat = int.Parse(seat.CountSeat); // Tổng số ghế của Seat

                if (soldCount >= maxSeat)
                {
                    return "Vé đã hết";
                }

                // Tạo ticket cho người dùng
                var ticket = new Ticket
                {
                    UserId = data.UserId,
                    SeatId = data.SeatId,
                    NumberSeatId = null, // Không cần NumberSeatId vì không có DetailSeat
                    Status = TicketStatus.Pending,
                    TypeTicket = TicketType.Standard,
                    QrImage = "", // Gọi hàm generate QR code
                    Date = DateTimeOffset.Now
                };

                _dbContext.Tickets.Add(ticket);
                await _dbContext.SaveChangesAsync();

                return "Đặt vé thành công";
            }
        }

        public class TicketRequest
        {
            public int EventId { get; set; }
            public int UserId { get; set; }
            public int SeatId { get; set; }
            public int? DetailSeatId { get; set; }  // Optional, chỉ cần nếu có DetailSeat
            public int? NumberSeatId { get; set; }  // Optional, chỉ cần nếu có DetailSeat
        }
    }
}