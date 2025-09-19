using Company.Project.Application.Contracts;
using Company.Project.DTO;
using Company.Project.DTO.DTO.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var paymentsDto = await _paymentService.GetAllPaymentsAsync();
                return View(paymentsDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load payments: " + ex.Message;
                return View(new List<PaymentDto>());
            }
        }

    }
}
