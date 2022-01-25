using Api_App_Flix.Data;
using Api_App_Flix.Models;
using Api_App_Flix.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App_Flix.Controllers
{

    [ApiController]
    [Route("v1/Users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromServices] AppDbContext context)
        {
            var All = await context.Users.AsNoTracking().ToListAsync();
            return Ok(All);
        }
        
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                element => element.Id == id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User()
            {
                Username = model.Username,
                Password = model.Password,
                Role = model.Role
            };
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"v1/Users/{user.Id}", user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }
        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromServices] AppDbContext context, 
            [FromBody] CreateUserViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();
            
            try
            {
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();
            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
               return BadRequest();
            }
            
        }
        
    }    
}
