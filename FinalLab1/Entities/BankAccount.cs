namespace FinalLab1.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string NameOwner { get; set; }
        public string NumberBank { get; set; }
        public int NameBank { get; set; }

        public Event? Event { get; set; }
    }
}