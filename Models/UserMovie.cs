namespace Api_App_Flix.Models;

public class UserMovie
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; }
}