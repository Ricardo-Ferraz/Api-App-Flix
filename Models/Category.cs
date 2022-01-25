namespace Api_App_Flix.Models;

public class Category
{
    public int Id { get; set; }
    public string nome { get; set; }
    
    public virtual List<MovieCategory> MovieCategories { get; set; }
}