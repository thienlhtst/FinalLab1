namespace FinalLab1.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = String.Empty;

        public ICollection<EventCategory>? EventCategories { get; set; } = new List<EventCategory>();
    }
    }
