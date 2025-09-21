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
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(ProductService productService , CategoryService categoryService , BrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
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

            if (dto.image != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath = Path.Combine(folder, fileName);
            }
            if (dto.image2 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image2.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image2.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath2 = Path.Combine(folder, fileName);
            }
            if (dto.image3 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image3.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image3.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath3 = Path.Combine(folder, fileName);
            }
            if (dto.image4 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image4.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image4.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath4 = Path.Combine(folder, fileName);
            }
            if (dto.image5 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image5.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image5.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath5 = Path.Combine(folder, fileName);
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
                Imagepath = product.Imagepath,
                Imagepath2 = product.Imagepath2,
                Imagepath3 = product.Imagepath3,
                Imagepath4 = product.Imagepath4,
                Imagepath5 = product.Imagepath5,
                CategoryId = product.Category?.Id ?? 0,
                BrandId = product.Brand?.Id ?? 0
            };

            await LoadDropDowns();

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns();
                return View(dto);
            }

            // Handle image uploads
            if (dto.image != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath = Path.Combine(folder, fileName);
            }
            if (dto.image2 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image2.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image2.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath2 = Path.Combine(folder, fileName);
            }
            if (dto.image3 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image3.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image3.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath3 = Path.Combine(folder, fileName);
            }
            if (dto.image4 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image4.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image4.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath4 = Path.Combine(folder, fileName);
            }
            if (dto.image5 != null)
            {
                string folder = "Images\\Product";
                string fileName = Guid.NewGuid().ToString() + "_" + dto.image5.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await dto.image5.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                dto.Imagepath5 = Path.Combine(folder, fileName);
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
