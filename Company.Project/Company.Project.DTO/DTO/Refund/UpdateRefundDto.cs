namespace Company.Project.DTO.DTO.Refund
{
    public class UpdateRefundDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public int OrderId { get; set; }
    }
}