using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class RefundController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}