using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Project.Domain.Models
{
    public class CartItem : BaseEntity
    {
        public int quantity { get; set; }

        [ForeignKey("product")]
        public int productId { get; set; }
        public Product product { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
