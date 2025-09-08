using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Refund;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService _refundService;

        public RefundController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
        {
            var refunds = await _refundService.GetAllAsync(skip, take);
            return Ok(refunds);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid refund ID.");
            }
            var refund = await _refundService.GetByIdAsync(id);
            if (refund == null)
            {
                return NotFound("Refund not found.");
            }
            return Ok(refund);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRefundDto createRefundDto)
        {
            if (createRefundDto == null || string.IsNullOrWhiteSpace(createRefundDto.Reason))
            {
                return BadRequest("Invalid refund data.");
            }
            var createdRefund = await _refundService.CreateAsync(createRefundDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRefund.Id }, createdRefund);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRefundDto updateRefundDto)
        {
            if (id <= 0 || updateRefundDto == null || id != updateRefundDto.Id)
            {
                return BadRequest("Invalid refund data.");
            }
            try
            {
                var updatedRefund = await _refundService.UpdateAsync(updateRefundDto);
                return Ok(updatedRefund);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Refund not found.");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid refund ID.");
            }
            var existingRefund = await _refundService.GetByIdAsync(id);
            if (existingRefund == null)
            {
                return NotFound("Refund not found.");
            }
            await _refundService.DeleteAsync(id);
            return NoContent();
        }
        
    }
}
