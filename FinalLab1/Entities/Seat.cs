namespace FinalLab1.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int EventSessionId { get; set; }
        public string Name { get; set; }
        public string CountSeat { get; set; }
        public string Type { get; set; }
        public string Information { get; set; }
        public DateTimeOffset Opentobuy { get; set; }
        public decimal Price { get; set; }
        

        public EventSession? EventSession { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<DetailSeat>? DetailSeats { get; set; }
    }
}