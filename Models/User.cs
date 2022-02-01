namespace Api_App_Flix.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime Registration { get; set; }= DateTime.Now;
    public string? UrlImagem { get; set; }= null;
    public string? Token { get; set; }
    
    public virtual List<UserMovie> UserMovies { get; set; }
}