using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Category;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Invalid category ID.");
            }
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _categoryService.SearchAsync(query);
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null || string.IsNullOrWhiteSpace(createCategoryDto.Name))
            {
                return BadRequest("Invalid category data.");
            }
            var createdCategory = await _categoryService.CreateAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (id <= 0 || updateCategoryDto == null || id != updateCategoryDto.Id)
            {
                return BadRequest("Invalid category data.");
            }
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }
            await _categoryService.UpdateAsync(updateCategoryDto);
            // return updated message
            return Ok("Category updated successfully.");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID.");
            }
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }
            await _categoryService.DeleteAsync(id);
            return Ok("Category deleted successfully.");
        }
        
    }
}

