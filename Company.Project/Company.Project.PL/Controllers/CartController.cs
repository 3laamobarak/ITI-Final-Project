using Company.Project.Application.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            var cart = await _cartItemService.GetUserCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userId = "1";
            await _cartItemService.AddToCartAsync(userId, productId, quantity);
            return Ok(new { message = "Product added to cart successfully." });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartItemService.UpdateQuantityAsync(userId, itemId, quantity);
            return Ok(new { message = "Quantity updated." });
        }

        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartItemService.RemoveFromCartAsync(userId, itemId);
            return Ok(new { message = "Item removed from cart." });
        }
    }
}
