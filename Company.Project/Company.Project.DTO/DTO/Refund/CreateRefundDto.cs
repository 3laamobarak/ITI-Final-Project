namespace Company.Project.DTO.DTO.Refund
{
    public class CreateRefundDto
    {
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public int OrderId { get; set; }
    }
}
