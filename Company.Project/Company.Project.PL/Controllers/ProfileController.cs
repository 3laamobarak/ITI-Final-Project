using System.Security.Claims;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userService.GetUserProfileAsync(userId);
            if (user == null)
                return NotFound();

            var dto = new UserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Gender = user.Gender?.ToString()
            };

            return Ok(dto);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await _userService.UpdateUserProfileAsync(userId, dto.FirstName, dto.PhoneNumber);
            if (!success) return BadRequest("Update failed");

            return Ok(new { Message = "Profile updated successfully" });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _userService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Password changed successfully" });
        }

        [HttpPost("update-email")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _userService.UpdateEmailAsync(userId, dto.NewEmail);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Email updated successfully" });
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _userService.DeleteAccountAsync(userId);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Account deleted successfully" });
        }
    }
}
