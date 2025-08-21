    namespace Company.Project.Domain.Models
    {
        public class Category : BaseEntity
        {
            public string Name { get; set; }
            public string Description { get; set; }

            // Navigation properties
            public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        
        }
    }
