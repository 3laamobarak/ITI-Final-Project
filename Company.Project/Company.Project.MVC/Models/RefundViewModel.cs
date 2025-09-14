namespace Company.Project.MVC.Models
{
    public class RefundViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }   
                public int PaymentId { get; set; }
        public string StatusText =>
            Status switch
            {
                0 => "Pending",
                1 => "Approved",
                2 => "Rejected",
                _ => "Unknown"
            };
    }
}
