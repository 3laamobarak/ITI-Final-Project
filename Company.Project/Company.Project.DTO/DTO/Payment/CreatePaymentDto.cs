using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.Payment
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
        public string Currency { get; set; }  
        public string ShippingAddress { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
