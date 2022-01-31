using System.ComponentModel.DataAnnotations;

namespace Api_App_Flix.ViewModels;

public class LoginUserViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    
}