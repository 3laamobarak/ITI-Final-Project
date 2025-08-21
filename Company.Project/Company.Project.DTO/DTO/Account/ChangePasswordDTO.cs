using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Account
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}
