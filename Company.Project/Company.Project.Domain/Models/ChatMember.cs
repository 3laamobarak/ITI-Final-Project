namespace Company.Project.Domain.Models
{
    public class ChatMember
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        
        
    }
}
