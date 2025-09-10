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
        public IActionResult GetAll(int skip = 0, int take = 20)
        {
            var result = _brandService.GetAllAsync(skip, take);          
            return View(result);
        }
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetByIdAsync(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBrandDto brand)
        {
            if (ModelState.IsValid)
            {
                _brandService.CreateAsync(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }
        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(UpdateBrandDto brand)
        {
            if (brand.Name != null && brand.Description != null)
            {
                _brandService.UpdateAsync(brand);
                return RedirectToAction("GetAll");
            }

            return View(brand);
        }
        public IActionResult Delete(int id)
        {
            _brandService.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
