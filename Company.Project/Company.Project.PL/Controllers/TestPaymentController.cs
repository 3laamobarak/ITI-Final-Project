using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestPaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("check-payments")]
        public async Task<IActionResult> CheckPayments()
        {
            try
            {
                var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
                var paymentCount = payments.Count();
                
                return Ok(new { 
                    message = "Payments retrieved successfully",
                    count = paymentCount,
                    payments = payments.Select(p => new {
                        p.Id,
                        p.Amount,
                        p.PaymentDate,
                        p.IsSuccessful,
                        p.PaymentIntentId,
                        p.OrderId,
                        p.UserId
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("check-orders")]
        public async Task<IActionResult> CheckOrders()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                var orderCount = orders.Count();
                
                return Ok(new { 
                    message = "Orders retrieved successfully",
                    count = orderCount,
                    orders = orders.Select(o => new {
                        o.Id,
                        o.Total,
                        o.Status,
                        o.OrderDate,
                        o.UserId,
                        o.ShippingAddress
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
