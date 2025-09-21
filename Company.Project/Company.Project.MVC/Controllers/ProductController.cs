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
                using (var ms = new MemoryStream())
                {
                    await dto.image.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image.ContentType + ";base64," + base64;
                    dto.Imagepath = base64;
                }
                
            }
            if (dto.image2 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image2.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image2.ContentType + ";base64," + base64;
                    dto.Imagepath2 = base64;
                }
                
            }
            if (dto.image3 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image3.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image3.ContentType + ";base64," + base64;
                    dto.Imagepath3 = base64;
                }
                
            }
            if (dto.image4 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image4.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image4.ContentType + ";base64," + base64;
                    dto.Imagepath4 = base64;
                }
                
            }
            if (dto.image5 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image5.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image5.ContentType + ";base64," + base64;
                    dto.Imagepath5 = base64;
                }
                
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
                // do the same here but with the update
                // using (var ms = new MemoryStream())
                // {
                //     await dto.image4.CopyToAsync(ms);
                //     var base64 = Convert.ToBase64String(ms.ToArray());
                //     base64 = "data:" + dto.image4.ContentType + ";base64," + base64;
                //     dto.Imagepath4 = base64;
                // }
                // like that
                using (var ms = new MemoryStream())
                {
                    await dto.image.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image.ContentType + ";base64," + base64;
                    dto.Imagepath = base64;
                }
            }
            if (dto.image2 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image2.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image2.ContentType + ";base64," + base64;
                    dto.Imagepath4 = base64;
                }
            }
            if (dto.image3 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image3.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image3.ContentType + ";base64," + base64;
                    dto.Imagepath3 = base64;
                }
                
            }
            if (dto.image4 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image4.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image4.ContentType + ";base64," + base64;
                    dto.Imagepath4 = base64;
                }
            }
            if (dto.image5 != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.image5.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    base64 = "data:" + dto.image5.ContentType + ";base64," + base64;
                    dto.Imagepath5 = base64;
                }
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
