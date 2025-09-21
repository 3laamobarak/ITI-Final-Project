using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            // direct to dashboard in controller
            return RedirectToAction("Index", "Dashboard");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
