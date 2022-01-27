namespace Api_App_Flix.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Age { get; set; }
    public int Rating { get; set; } // 1 a 5
    public string Description { get; set; }
    public string Cast { get; set; }
    public string Duration { get; set; }
    public string UrlImagem { get; set; }
    public virtual List<UserMovie> UserMovies { get; set; }
    public virtual List<MovieCategory> MovieCategories { get; set; }
}