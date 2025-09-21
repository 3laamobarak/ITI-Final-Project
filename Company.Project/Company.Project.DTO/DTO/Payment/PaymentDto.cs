using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsSuccessful { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal RefundedAmount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
    }
}