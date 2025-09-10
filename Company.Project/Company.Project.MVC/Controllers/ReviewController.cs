using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class ReviewController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}