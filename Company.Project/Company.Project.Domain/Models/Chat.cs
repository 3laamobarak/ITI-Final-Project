namespace Company.Project.Domain.Models
{
    public class Chat : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
