using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.DTO.DTO.Payment;

namespace Company.Project.Application.Contracts
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(CreatePaymentDto dto, string userId);
        Task<bool> ConfirmPaymentAsync(string paymentIntentId);
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<bool> RefundPaymentAsync(int paymentId, decimal amount);
    }
}
