namespace FinalLab1.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<EventSession>? EventSessions { get; set; } = new List<EventSession>();
    }
}