using System.ComponentModel.DataAnnotations;

namespace Company.Project.MVC.Models;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string NID { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage ="Password and Confirm Password do not match")]
    public string ConfirmPassword { get; set; }        
    
}