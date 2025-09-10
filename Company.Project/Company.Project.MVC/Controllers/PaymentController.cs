using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class PaymentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}