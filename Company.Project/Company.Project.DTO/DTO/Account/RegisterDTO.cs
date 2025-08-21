using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "NID is required.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "NID must be a 14-digit number.")]
        public string NID { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string OtpCode { get; set; }
    }
}