using System.ComponentModel.DataAnnotations.Schema;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Domain.Models
{
    public class ChatMessage : BaseEntity
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsFromAdmin { get; set; }
        public bool IsRead { get; set; }= false;
    }
}
