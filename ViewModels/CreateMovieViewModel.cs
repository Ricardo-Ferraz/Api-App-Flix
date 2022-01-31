using System.ComponentModel.DataAnnotations;

namespace Api_App_Flix.ViewModels
{
    public class CreateMovieViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public int Rating { get; set; } // 1 a 5
        [Required]
        public string Description { get; set; }
        [Required]
        public string Cast { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string UrlImagem { get; set; }
    }
}