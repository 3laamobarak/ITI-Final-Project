using System.ComponentModel.DataAnnotations;

namespace Company.Project.MVC.Models;

public class LoginViewModel
{
    [Required]
    public string UserName { set; get; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { set; get; }
    public bool RemmemberMe { set; get; }
    
}