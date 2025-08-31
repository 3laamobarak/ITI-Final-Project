using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Application.Contracts;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصرح له");
            try
            {
                var cart = await _cartItemService.GetUserCartAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصرح له");
            try
            {
                await _cartItemService.AddToCartAsync(userId, request.ProductId, request.Quantity);
                return Ok(new { message = "تم إضافة المنتج للعربية بنجاح" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityRequest request)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصرح له");
            try
            {
                await _cartItemService.UpdateQuantityAsync(userId, request.ItemId, request.Quantity);
                return Ok(new { message = "تم تحديث الكمية بنجاح" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصرح له");
            try
            {
                await _cartItemService.RemoveFromCartAsync(userId, itemId);
                return Ok(new { message = "تم إزالة المنتج من العربية" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصرح له");
            try
            {
                await _cartItemService.ClearCartAsync(userId);
                return Ok(new { message = "تم تفريغ العربية بنجاح" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateQuantityRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}