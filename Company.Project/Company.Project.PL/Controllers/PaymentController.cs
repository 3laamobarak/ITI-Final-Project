using System.Security.Claims;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Payment;
using Company.Project.DTO.DTO.Refund;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Project.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            if (dto == null || dto.Amount <= 0 || dto.CartItems == null || !dto.CartItems.Any())
                return BadRequest("Invalid payment data.");

            if (string.IsNullOrWhiteSpace(dto.ShippingAddress))
                return BadRequest("Shipping address is required.");

            try
            {
                var clientSecret = await _paymentService.CreatePaymentIntentAsync(dto, userId);
                return Ok(new { clientSecret });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment creation failed for user {UserId}", userId);
                return StatusCode(500, "Failed to create payment intent.");
            }
        }

        [HttpPost("confirm/{paymentIntentId}")]
        public async Task<IActionResult> ConfirmPayment(string paymentIntentId)
        {
            if (string.IsNullOrEmpty(paymentIntentId))
                return BadRequest("PaymentIntentId is required.");

            try
            {
                var isSuccess = await _paymentService.ConfirmPaymentAsync(paymentIntentId);
                if (isSuccess)
                {
                }
                return Ok(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment confirmation failed for PaymentIntentId {PaymentIntentId}", paymentIntentId);
                return StatusCode(500, "Failed to confirm payment.");
            }
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllPaymentsAsync(); 
                return Ok(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch payments");
                return StatusCode(500, "Failed to fetch payments");
            }
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment([FromBody] RefundPaymentDto dto)
        {
            if (dto == null || dto.PaymentId <= 0 || dto.Amount <= 0)
                return BadRequest("Invalid refund data.");

            try
            {
                if (_paymentService is StripePaymentService stripeService)
                {
                    var success = await stripeService.RefundPaymentAsync(dto.PaymentId, dto.Amount);
                    if (success)
                        return Ok(new { Message = "Refund successful" });
                    return BadRequest("Refund failed");
                }
                else
                {
                    return BadRequest("Refund not supported in this payment service");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Refund failed: {ex.Message}");
            }
        }

    }
}
