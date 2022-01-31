using System.ComponentModel.DataAnnotations;

namespace Api_App_Flix.ViewModels;

public class CreateUserViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string UrlImagem { get; set; }
}