using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Company.Project.Domain.Models
{
    public class ChatBotMessages : BaseEntity
    {

        [Required]
        public string Message { get; set; }

        /// "User" or "Bot" — you can replace with an enum later if you like
        [Required]
        [MaxLength(20)]
        public string Sender { get; set; }


        // FK to Identity user
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
