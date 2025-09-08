using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Project.Domain.Models
{
    public class Review : BaseEntity
    {
        public string Comment { get; set; }
        public decimal Rating { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
