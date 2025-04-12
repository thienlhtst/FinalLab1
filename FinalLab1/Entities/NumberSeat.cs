namespace FinalLab1.Entities
{
    public class NumberSeat
    {
        public int Id { get; set; }
        public int DetailSeatId { get; set; }
        public int Number { get; set; }

        public DetailSeat? DetailSeat { get; set; }
        public Ticket? Ticket { get; set; }
    }
}