using Company.Project.Application.Services;
using Company.Project.DTO.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // Get all
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // Get by id
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // create
        public async Task<IActionResult>  Create()
        {
            await LoadDropDowns();

            return View();
        }

        // create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns();
                return View(dto);
            }

            await _productService.CreateAsync(dto);
            return RedirectToAction(nameof(GetAll));
        }

        // update
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var updateDto = new UpdateProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                Overview = product.Overview,
                SuggestedUse = product.SuggestedUse,
                Warnings = product.Warnings,
                Disclaimer = product.Disclaimer,
                ExpiryDate = product.ExpiryDate,
                ImageUrl = product.ImageUrl,
                Imageone = product.Imageone,
                Imagetwo = product.Imagetwo,
                Imagethree = product.Imagethree,
                Imagefour = product.Imagefour,
                CategoryId = product.Category?.Id ?? 0,
                BrandId = product.Brand?.Id ?? 0

            };

            await LoadDropDowns();

            return View(updateDto);
        }

        // UPDATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns();
                return View(dto);
            }

            await _productService.UpdateAsync(dto);

            return RedirectToAction(nameof(GetAll));
        }

        // delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(GetAll));
        }


        //  helper
        private async Task LoadDropDowns()
        {
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Brands = new SelectList(brands, "Id", "Name");
        }


    }
}
