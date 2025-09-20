using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.Review
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
