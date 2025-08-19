namespace Company.Project.Domain.Models
{
    public class OTP
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsUsed { get; set; }
        public string? userId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
