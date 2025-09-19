using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Refund;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRefundDto createRefundDto)
        {
            if (createRefundDto == null || string.IsNullOrWhiteSpace(createRefundDto.Reason))
            {
                return BadRequest("Invalid refund data.");
            }

            var createdRefund = await _refundService.CreateAsync(createRefundDto);
            return CreatedAtAction(nameof(Create), new { id = createdRefund.Id }, createdRefund);
        }
    }
}
