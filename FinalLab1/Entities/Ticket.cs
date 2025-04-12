namespace FinalLab1.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public int? NumberSeatId { get; set; }
        public TicketStatus Status { get; set; }
        public TicketType TypeTicket { get; set; }
        public string QrImage { get; set; }
        public DateTimeOffset Date { get; set; }

        public User? User { get; set; }
        public Seat? Seat { get; set; }
        public NumberSeat? NumberSeat { get; set; }
        public Payment? Payment { get; set; }
    }
}