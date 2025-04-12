namespace FinalLab1.Entities
{
    public class SpecialEvent
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }

        public Event? Event { get; set; }
    }
}