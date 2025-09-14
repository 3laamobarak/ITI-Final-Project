using Company.Project.Application.Contracts;
using Company.Project.Application.Services;
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
       
        // GET ALL
        public async Task<IActionResult> GetAll(int skip = 0, int take = 20)
        {
            var result = await _CategoryService.GetAllAsync();

            return View("GetAll",result);
        }
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _CategoryService.GetByIdAsync(id);


            return View("Details", category); 
        }
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto Category)
        {
            if (ModelState.IsValid)
            {
                await _CategoryService.CreateAsync(Category);

                return RedirectToAction("GetAll");
            }
            return View("Create",Category);
        }
        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _CategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("Edit",category);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto Category)
        {
            if (ModelState.IsValid)
            {
                await _CategoryService.UpdateAsync(Category);

                return RedirectToAction("GetAll");
            }

            return View(Category);
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _CategoryService.DeleteAsync(id);

            return RedirectToAction("GetAll");
        }
    }
}
