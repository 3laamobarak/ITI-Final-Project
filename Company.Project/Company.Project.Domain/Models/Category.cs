namespace Company.Project.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}
