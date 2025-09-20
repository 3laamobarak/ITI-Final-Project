using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers;

public class ChatController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}