using System.Security.Claims;
using Company.Project.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class CartItemController : Controller
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    // GET ALL (User Cart)
    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId))

            return Unauthorized();

        var cartItems = await _cartItemService.GetUserCartAsync(userId);

        return View(cartItems);
    }

    // ADD TO CART
    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId)) 

            return Unauthorized();

        await _cartItemService.AddToCartAsync(userId, productId, quantity);

        return RedirectToAction("Index");
    }

    // UPDATE QUANTITY
    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId))

            return Unauthorized();

        await _cartItemService.UpdateQuantityAsync(userId, itemId, quantity);

        return RedirectToAction("Index");
    }

    // REMOVE ITEM
    public async Task<IActionResult> Remove(int id)
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId))

            return Unauthorized();

        await _cartItemService.RemoveFromCartAsync(userId, id);

        return RedirectToAction("Index");
    }

    // CLEAR CART
    public async Task<IActionResult> Clear()
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId))

            return Unauthorized();

        await _cartItemService.ClearCartAsync(userId);

        return RedirectToAction("Index");
    }
}
