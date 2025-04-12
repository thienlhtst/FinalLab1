namespace FinalLab1.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public int MethodId { get; set; }
        public DateTimeOffset PaidAt { get; set; }
        public PaymentStatus Status { get; set; }

        public User? User { get; set; }
        public Ticket? Ticket { get; set; }
        public Method? Method { get; set; }
    }
}