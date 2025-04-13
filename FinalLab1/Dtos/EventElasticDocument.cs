namespace FinalLab1.Dtos
{
    public class EventElasticDocument
    {
        public string Id { get; set; }

        public string EventName { get; set; }
        public string Information { get; set; }

        public string OrganizerName { get; set; }

        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationCountry { get; set; }

        public List<string> CategoryNames { get; set; } = new();

        public DateTimeOffset? FromTime { get; set; }
    }
}