using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Refund;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class RefundController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IRefundService _refundService;

        public RefundController(IPaymentService paymentService, IRefundService refundService)
        {
            _paymentService = paymentService;
            _refundService = refundService;
        }

        public async Task<IActionResult> Index(string filter = "all")
        {
            try
            {
                var refunds = (await _refundService.GetAllAsync()).ToList();

                if (filter == "active")
                    refunds = refunds.Where(r => !r.IsDeleted).ToList();
                else if (filter == "deleted")
                    refunds = refunds.Where(r => r.IsDeleted).ToList();
                return View(refunds); 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to load refunds: {ex.Message}";
                return View(new List<RefundDto>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefundPayment(int id)
        {
            try
            {
                var refund = (await _refundService.GetAllAsync()).FirstOrDefault(r => r.Id == id);
                if (refund == null)
                {
                    TempData["ErrorMessage"] = "Refund not found.";
                    return RedirectToAction("Index");
                }

                var success = await _paymentService.RefundPaymentAsync(refund.PaymentId, refund.Amount);
                if (success)
                {
                    refund.Status = (int)Company.Project.Domain.Enums.Enums.RefundStatus.Completed;

                    await _refundService.UpdateAsync(new UpdateRefundDto
                    {
                        Id = refund.Id,
                        Status = (Company.Project.Domain.Enums.Enums.RefundStatus)refund.Status,
                        ProcessedDate = DateTime.UtcNow
                    });

                    TempData["SuccessMessage"] = "Refund processed successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Refund failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _refundService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Refund deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
