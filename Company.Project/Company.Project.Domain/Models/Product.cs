using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Project.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        
        public decimal AverageRating
        {
            get
            {
                if (Reviews.Count == 0) return 0;
                return Reviews.Average(r => r.Rating);
            }
        }
        public int ReviewCount
        {
            get { return Reviews.Count; }
        }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
    }
}
