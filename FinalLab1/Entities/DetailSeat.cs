namespace FinalLab1.Entities
{
    public class DetailSeat
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public string Section { get; set; }

        public Seat? seat { get; set; }
        public ICollection<NumberSeat>? NumberSeats { get; set; } = new List<NumberSeat>();
    }
}