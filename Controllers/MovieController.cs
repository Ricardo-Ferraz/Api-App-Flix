using Api_App_Flix.Data;
using Api_App_Flix.Models;
using Api_App_Flix.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App_Flix.Controllers
{

    [ApiController]
    [Route("v1/Movies")]
    public class MovieController : ControllerBase
    {
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromServices] AppDbContext context)
        {   
            var All = await context.Movies.AsNoTracking().ToListAsync();

            return Ok(All);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {

            try
            {
                var movie = await context.Movies.AsNoTracking().FirstOrDefaultAsync(
                    element => element.Id == id);
                if (movie != null)
                {
                    return Ok(movie);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var test = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                element => element.Username.Equals(model.Title));
          
            if(test != null) return BadRequest();

            var movie = new Movie()
            {
                Title= model.Title,
                Director= model.Director,
                Age= model.Age,
                Rating= model.Rating,
                Description= model.Description,
                Cast= model.Cast,
                Duration= model.Duration,
                UrlImagem= model.UrlImagem     
            };
            try
            {
                await context.Movies.AddAsync(movie);
                await context.SaveChangesAsync();
                return Created($"v1/Users/{movie.Id}", movie);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromServices] AppDbContext context, 
            [FromBody] UpdateMovieViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null) return NotFound();
            
            try
            {
                if(model.Title != "" && model.Title != null ) movie.Title= model.Title;
                if(model.Director != "" && model.Director != null ) movie.Director= model.Director;
                if(model.Age != "" && model.Age != null ) movie.Age= model.Age;
                if(model.Rating > 0) movie.Rating= model.Rating;
                if(model.Description != "" && model.Description != null ) movie.Description= model.Description;
                if(model.Cast != "" && model.Cast != null ) movie.Cast= model.Cast;
                if(model.Duration != "" && model.Duration != null ) movie.Duration= model.Duration;
                if(model.UrlImagem != "" && model.UrlImagem != null ) movie.UrlImagem= model.UrlImagem;
                
                context.Movies.Update(movie);
                await context.SaveChangesAsync();

                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null) return NotFound();
            try
            {
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return Ok(movie);
            }
            catch (Exception e)
            {
               return BadRequest();
            }
            
        }
    }
}