using Newtonsoft.Json;

namespace FinalLab1.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } = Role.User;
        public DateTimeOffset Birthday { get; set; } 
        public Gender Gender { get; set; } = Gender.Male;

        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Event>? OrganizedEvents { get; set; }
    }
}