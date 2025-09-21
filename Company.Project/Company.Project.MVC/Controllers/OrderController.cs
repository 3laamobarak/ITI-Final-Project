using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.MVC.Controllers
{
    [Authorize(Policy = "admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(
                includes: new System.Linq.Expressions.Expression<Func<Order, object>>[]
                {
                    o => o.User,
                    o => o.OrderItems
                }
            );
            
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByExpressionSingleAsync(
                o => o.Id == id,
                includes: new System.Linq.Expressions.Expression<Func<Order, object>>[]
                {
                    o => o.User,
                    o => o.OrderItems,
                    o => o.Payments,
                    o => o.Refunds
                }
            );

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.OrderTypes = Enum.GetValues<OrderType>();
            ViewBag.OrderStatuses = Enum.GetValues<OrderStatus>();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreatedAt = DateTime.UtcNow;
                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.Completeasync();
                TempData["SuccessMessage"] = "Order created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.OrderTypes = Enum.GetValues<OrderType>();
            ViewBag.OrderStatuses = Enum.GetValues<OrderStatus>();
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.OrderTypes = Enum.GetValues<OrderType>();
            ViewBag.OrderStatuses = Enum.GetValues<OrderStatus>();
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.Completeasync();
                    TempData["SuccessMessage"] = "Order updated successfully.";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the order.";
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.OrderTypes = Enum.GetValues<OrderType>();
            ViewBag.OrderStatuses = Enum.GetValues<OrderStatus>();
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByExpressionSingleAsync(
                o => o.Id == id,
                includes: new System.Linq.Expressions.Expression<Func<Order, object>>[]
                {
                    o => o.User,
                    o => o.OrderItems
                }
            );

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order != null)
                {
                    await _unitOfWork.OrderRepository.DeleteAsync(order);
                    await _unitOfWork.Completeasync();
                    TempData["SuccessMessage"] = "Order deleted successfully.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the order.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Order/UpdateStatus/5
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.OrderStatuses = Enum.GetValues<OrderStatus>();
            return View(order);
        }

        // POST: Order/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order != null)
                {
                    order.Status = status;
                    order.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.Completeasync();
                    TempData["SuccessMessage"] = "Order status updated successfully.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the order status.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Order/GetByUser/{userId}
        public async Task<IActionResult> GetByUser(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserName = user?.UserName ?? "Unknown User";
            return View("Index", orders);
        }
    }
}
