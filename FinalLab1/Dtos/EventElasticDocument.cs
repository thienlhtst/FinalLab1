namespace FinalLab1.Dtos
{
    public class EventElasticDocument
    {
        public string Id { get; set; } = default!;
        public string EventName { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string CategoryNames { get; set; } = string.Empty;
        public DateTimeOffset FromTime { get; set; }
        public string EventType { get; set; } = string.Empty;
    }
}