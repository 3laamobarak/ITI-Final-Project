namespace Company.Project.MVC.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsSuccessful { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
