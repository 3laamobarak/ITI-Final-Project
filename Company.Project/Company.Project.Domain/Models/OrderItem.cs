using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Project.Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal SubTotal
        {
            get { return Quantity * Product.Price; }
        }

    }
}
