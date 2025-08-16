using System.ComponentModel.DataAnnotations.Schema;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Domain.Models
{
    public class ChatMessage : BaseEntity
    {

        public string MessageContent { get; set; }
        public MessageType MessageType { get; set; }

        [ForeignKey("User")]
        public string SenderId { get; set; }
        public ApplicationUser User { get; set; }
        
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        
        public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
        
        
    }
}
