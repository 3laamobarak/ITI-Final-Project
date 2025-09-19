using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.BrandDTO;
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
        // GET ALL
        public async Task<IActionResult> GetAll(int skip = 0, int take = 20)
        {
            var result = await _brandService.GetAllAsync(skip, take);

            return View(result);
        }

        // GET BY ID (Details)
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _brandService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return View("Details", result);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View("Create");
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandDto brand)
        {
            if (ModelState.IsValid)
            {
                await _brandService.CreateAsync(brand);
                return RedirectToAction("GetAll");
            }
            return View(brand);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var updateBrand = new UpdateBrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description

            };
            return View("Edit", updateBrand);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBrandDto brand)
        {
            if (brand.Name != null && brand.Description != null)
            {
              await _brandService.UpdateAsync(brand);
                return RedirectToAction("GetAll");
            }

            return View(brand);
        }
        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
