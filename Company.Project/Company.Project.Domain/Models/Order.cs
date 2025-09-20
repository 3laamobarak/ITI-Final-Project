using System.ComponentModel.DataAnnotations.Schema;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Domain.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string ShippingAddress { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total
        {
            get { return Subtotal + Tax + ShippingCost - Discount; }
        }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}