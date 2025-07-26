using System.ComponentModel.DataAnnotations;

namespace Company.Project.Domain.Models
{
    public class ChatMember
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the chat member
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
