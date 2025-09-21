using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Account;
using Company.Project.DTO.DTO.OTPs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Engines;

namespace Company.Project.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IOTPService _otpService;
        private readonly IEmailService _emailService;
        
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config, IOTPService otpService, IEmailService emailService)
        {
            this.userManager = userManager;
            this.config = config;
            _otpService = otpService;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(userDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists.");
                }
                //var isOtpValid = await _otpService.ValidateOTPAsync(userDto.Email, userDto.OtpCode);
                //if (!isOtpValid)
                //{
                //    return BadRequest("Invalid OTP code.");
                //}
                var sendOtp = await _otpService.GenerateAndSendOTPAsync(userDto.Email);
                var user = new ApplicationUser
                {
                    UserName = userDto.UserName,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    NID = userDto.NID,
                    Email = userDto.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(user);
                    
                    return Ok(new{Token = token});
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginDto.UserNameOrEmail) 
                           ?? await userManager.FindByNameAsync(loginDto.UserNameOrEmail);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginDto.Password);
                    if (found)
                    {
                        var token = GenerateJwtToken(user);
                        return Ok(new { Token = token });
                    }
                }
                return Unauthorized("Invalid username or password.");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO cpDto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(cpDto.Email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                
                var result = await userManager.ChangePasswordAsync(user, cpDto.CurrentPassword, cpDto.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    return Ok("Password changed successfully.");
                }
                return BadRequest("Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Request-Reset-Password")]
        [Authorize]
        public async Task<IActionResult> RequestPasswordReset([FromBody] EmailDTO emailDto)
        {
            var user = await userManager.FindByEmailAsync(emailDto.To);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{config["FrontendUrl"]}/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";
            await _emailService.SendEmailAsync(new EmailDTO
            {
                To = emailDto.To,
                Subject = "3mk 3laa sent u a Password Reset Request",
                Body =
                    $"Please click the following link to reset your password: <a href=\"{resetLink}\">Reset Password</a>"
            });
            return Ok("Password reset link has been sent to your email.");
        }
        [Authorize]
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetDto)
        {
            var user = await userManager.FindByEmailAsync(resetDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await userManager.ResetPasswordAsync(user, resetDto.Token, resetDto.NewPassword);
            
            if (result.Succeeded)
            {
                return Ok("Password reset successfully.");
            }

            return BadRequest(result.Errors);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> myclaims = new List<Claim>();
            myclaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            myclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            myclaims.Add(new Claim(ClaimTypes.Email, user.Email));
            myclaims.Add(new Claim("FirstName", user.FirstName));
            myclaims.Add(new Claim("LastName", user.LastName));
            myclaims.Add(new Claim("NID", user.NID));
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: myclaims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
        
    }
}
