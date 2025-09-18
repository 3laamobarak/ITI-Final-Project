
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.NutritionFact;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionFactController : ControllerBase
    {
        private readonly INutritionFactService _nutritionFactService;

        public NutritionFactController(INutritionFactService nutritionFactService)
        {
            _nutritionFactService = nutritionFactService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 100)
        {
            var nutritionFacts = await _nutritionFactService.GetAllAsync(skip, take);
            return Ok(nutritionFacts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid nutrition fact ID.");
            }
            var nutritionFact = await _nutritionFactService.GetByIdAsync(id);
            if (nutritionFact == null)
            {
                return NotFound("Nutrition fact not found.");
            }
            return Ok(nutritionFact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNutritionFactDto createNutritionFactDto)
        {
            if (createNutritionFactDto == null || string.IsNullOrWhiteSpace(createNutritionFactDto.Nutrient))
            {
                return BadRequest("Invalid nutrition fact data.");
            }
            var createdNutritionFact = await _nutritionFactService.CreateAsync(createNutritionFactDto);
            return CreatedAtAction(nameof(GetById), new { id = createdNutritionFact.Id }, createdNutritionFact);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateNutritionFactDto updateNutritionFactDto)
        {
            if (id <= 0 || updateNutritionFactDto == null || id != updateNutritionFactDto.Id)
            {
                return BadRequest("Invalid nutrition fact data.");
            }
            var existingNutritionFact = await _nutritionFactService.GetByIdAsync(id);
            if (existingNutritionFact == null)
            {
                return NotFound("Nutrition fact not found.");
            }
            await _nutritionFactService.UpdateAsync(updateNutritionFactDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid nutrition fact ID.");
            }

            var existingNutritionFact = await _nutritionFactService.GetByIdAsync(id);
            if (existingNutritionFact == null)
            {
                return NotFound("Nutrition fact not found.");
            }

            await _nutritionFactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
