using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.DTO.DTO.Refund
{
    public class UpdateRefundDto
    {
        public int Id { get; set; }
        public RefundStatus Status { get; set; }
        public int? AdminId { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}