using Company.Project.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly BrandService _brandService;

        public ProductController(ProductService productService , CategoryService categoryService , BrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> GetAll()
        {
            var prods = _productService.GetAllAsync();

            return View(prods);
        }
    }
}
