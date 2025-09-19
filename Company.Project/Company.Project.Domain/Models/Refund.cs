using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Domain.Models
{
    public class Refund : BaseEntity
    {
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public RefundStatus Status { get; set; } = RefundStatus.Pending;

        // Navigation properties
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Additional properties can be added as needed

        //public int PaymentId { get; set; }
        //public Payment Payment { get; set; }


    }
}
