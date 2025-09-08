using System.ComponentModel.DataAnnotations.Schema;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Domain.Models
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsSuccessful { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string PaymentIntentId { get; set; } // <--- ??? ??

    }

}
