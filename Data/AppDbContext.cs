using Api_App_Flix.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_App_Flix.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; } 
    public DbSet<Category> Categories { get; set; }
    public DbSet<MovieCategory> MovieCategories { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Password=1234;Persist Security Info=True;User ID=ricardo;Initial Catalog=Db-AppFlix;Data Source=localhost;");
    }
}