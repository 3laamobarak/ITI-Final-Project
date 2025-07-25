using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NID { get; set; }
        public string Gender { get; set; }
        public string? MaritalStatus { get; set; }
        
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
        

    }
}
