using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.BrandDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
        {
            var brands = await _brandService.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query) =>
          Ok(await _brandService.SearchAsync(query));
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandDto createBrandDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBrand = await _brandService.CreateAsync(createBrandDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBrand.Id }, createdBrand);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto updateBrandDto)
        {
            if (!ModelState.IsValid || updateBrandDto.Id != id) return BadRequest(ModelState);
            try
            {
                var updatedBrand = await _brandService.UpdateAsync(updateBrandDto);
                if (updatedBrand == null)
                {
                    return NotFound("Brand not found.");
                }
                return Ok(updatedBrand);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Brand not found.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _brandService.DeleteAsync(id);
                return Ok("Brand deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Brand not found.");
            }
        }

    }
}
