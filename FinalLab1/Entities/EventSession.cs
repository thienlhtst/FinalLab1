namespace FinalLab1.Entities
{
    public class EventSession
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int LocationId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public int MaxSeat { get; set; }
        public EventSessionType Type { get; set; }

        public Event? Event { get; set; }
        public Location? Location { get; set; }
        public ICollection<Seat>? Seats { get; set; } =new List<Seat>();
    }
}