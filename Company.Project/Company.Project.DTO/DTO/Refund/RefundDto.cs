using System;

namespace Company.Project.DTO.DTO.Refund
{
    public class RefundDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }

        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
