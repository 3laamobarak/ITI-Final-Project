using Company.Project.Application.Contracts;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        // GET /api/products/{productId}/reviews
        [HttpGet("products/{productId}/reviews")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await _reviewService.GetProductReviewsAsync(productId);
            return Ok(reviews);
        }

        // POST /api/products/{productId}/reviews
        [Authorize]
        [HttpPost("products/{productId}/reviews")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] ReviewDto dto)
        {
            var userId = _userManager.GetUserId(User);
            await _reviewService.AddReviewAsync(productId, dto, userId);
            return Ok();
        }

        // DELETE /api/reviews/{id}
        [Authorize]
        [HttpDelete("reviews/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _reviewService.DeleteReviewAsync(id, userId);
            if (!success)
                return Unauthorized("You can only delete your own reviews.");
            return NoContent();
        }

        // GET /api/reviews/user-reviews
        [Authorize]
        [HttpGet("user-reviews")]
        public async Task<IActionResult> GetUserReviews()
        {
            var userId = _userManager.GetUserId(User);
            var reviews = await _reviewService.GetUserReviewsAsync(userId);
            return Ok(reviews);
        }

        // PUT /api/reviews/{id}
        [Authorize]
        [HttpPut("reviews/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto dto)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _reviewService.UpdateReviewAsync(id, dto, userId);
            if (!success)
                return Unauthorized("You can only update your own reviews.");
            return Ok();
        }
    }
}
