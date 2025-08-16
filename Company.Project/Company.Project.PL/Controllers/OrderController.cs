using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Order;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Application.Contracts;

namespace Company.Project.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderSevice _orderService;
        private readonly UserManager<ApplicationUser> _userManager;


        public OrderController(IOrderSevice orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDto dto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = await _orderService.PlaceOrderAsync(dto, userId);
            return Ok(new { OrderId = orderId });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersForUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetOrderByIdAsync(id, userId);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}
