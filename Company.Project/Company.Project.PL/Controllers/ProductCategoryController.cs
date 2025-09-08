using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
        {
            var categories = await _productCategoryService.GetAllAsync(skip, take);
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID.");
            }
            var category = await _productCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }
            var createdCategory = await _productCategoryService.CreateAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.ProductId }, createdCategory);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCategoryDto updateCategoryDto)
        {
            if (id <= 0 || updateCategoryDto == null || id != updateCategoryDto.ProductId)
            {
                return BadRequest("Invalid category data.");
            }
            try
            {
                var updatedCategory = await _productCategoryService.UpdateAsync(updateCategoryDto);
                return Ok(updatedCategory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID.");
            }
            try
            {
                await _productCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found.");
            }
        }
    }
}