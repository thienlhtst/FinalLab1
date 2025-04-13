using FinalLab1.Entities;

namespace FinalLab1.Dtos
{
    public class ResultEventSearchDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string LogoImage { get; set; }
        public string BannerImage { get; set; }
        public int MaxSeatsPerUser { get; set; }

        public string UserName { get; set; }

        public List<EventSessionDto> EventSessions { get; set; } = new();
        public List<OrganizerDto> Organizers { get; set; } = new();
        public List<string> CategoryNames { get; set; } = new();
    }

    public class EventSessionDto
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public int MaxSeat { get; set; }
        public EventSessionType Type { get; set; }

        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class OrganizerDto
    {
        public string Name { get; set; }
        public string Information { get; set; }
        public string LogoImage { get; set; }
    }
}