using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            //
            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.Imagepath))
                {
                    product.Imagepath = $"{product.Imagepath}";
                }
            }
            //
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _productService.SearchAsync(query);
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null || string.IsNullOrWhiteSpace(createProductDto.Name))
            {
                return BadRequest("Invalid product data.");
            }
            var createdProduct = await _productService.CreateAsync(createProductDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (id <= 0 || updateProductDto == null || id != updateProductDto.Id)
            {
                return BadRequest("Invalid product data.");
            }
            try
            {
                var updatedProduct = await _productService.UpdateAsync(updateProductDto);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }
            await _productService.DeleteAsync(id);
            return Ok("Product deleted successfully.");
        }
    }

}
