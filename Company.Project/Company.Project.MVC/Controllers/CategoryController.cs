using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Category;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public IActionResult GetAll(int skip = 0, int take = 20)
        {
            var result = _CategoryService.GetAllAsync();          
            return View(result);
        }
        public IActionResult GetById(int id)
        {
            var result = _CategoryService.GetByIdAsync(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryDto Category)
        {
            if (ModelState.IsValid)
            {
                _CategoryService.CreateAsync(Category);
                return RedirectToAction("GetAll");
            }
            return View(Category);
        }
        public IActionResult Edit(int id)
        {
            var Category = _CategoryService.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }
        [HttpPost]
        public IActionResult Edit(UpdateCategoryDto Category)
        {
            if (Category.Name != null && Category.Description != null)
            {
                _CategoryService.UpdateAsync(Category);
                return RedirectToAction("GetAll");
            }

            return View(Category);
        }
        public IActionResult Delete(int id)
        {
            _CategoryService.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
