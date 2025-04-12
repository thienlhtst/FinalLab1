namespace FinalLab1.Entities
{
    public class Organizer
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string LogoImage { get; set; }

        public Event? Event { get; set; }
    }
}