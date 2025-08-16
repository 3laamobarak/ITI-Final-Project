using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.Order
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
