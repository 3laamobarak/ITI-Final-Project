using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class CartItemController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}