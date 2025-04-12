using Newtonsoft.Json;

namespace FinalLab1.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string LogoImage { get; set; }
        public string BannerImage { get; set; }
        public int CountEvent { get; set; }
        public int MaxSeatsPerUser { get; set; }
        public EventStatus Status { get; set; }

        public User? Organizer { get; set; }
        public ICollection<EventSession>? EventSessions { get; set; } = new List<EventSession>();
        public ICollection<EventCategory>? Categories { get; set; } = new List<EventCategory>();
        public ICollection<BankAccount>? BankAccounts { get; set; } = new List<BankAccount>();
        public ICollection<Organizer>? Organizers { get; set; } = new List<Organizer>();
        public ICollection<SpecialEvent>? SpecialEvent { get; set; } = new List<SpecialEvent>();
    }
}