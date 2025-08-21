using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Account
{
    public class ResetPasswordDTO
    {
        [Required (ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Token is required.")]
        public string Token { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        
    }
}
