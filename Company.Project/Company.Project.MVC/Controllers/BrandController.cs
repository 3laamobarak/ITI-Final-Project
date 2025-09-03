using Company.Project.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll(int skip = 0, int take = 10)
        {
            var result = _brandService.GetAllAsync(skip, take);          
            return View(result);
        }



    }
}
