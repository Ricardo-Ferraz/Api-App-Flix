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
            foreach (var user in All)
            {
                user.Password = "";
            }
            return Ok(All);
        }
        
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {

            try
            {
                var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                    element => element.Id == id);
                if (user != null)
                {
                    user.Password = "";
                    return Ok(user);
                }

                return NotFound("Usuário não encontrado");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao acessar o banco");
            }
        }
        
        [HttpGet]
        [Route("GetByUsername/{username}")]
        public async Task<IActionResult> GetByNameAsync([FromServices] AppDbContext context, [FromRoute] string username)
        {
            
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                element => element.Username.Equals(username));
            return user != null ? Ok(user) : NotFound("Usuário não encontrado");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromServices] AppDbContext context, [FromBody] LoginUserViewModel model
            )
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                element => element.Username.Equals(model.Username) && element.Password.Equals(model.Password));
            if (user != null)
            {
                user.Password = "";
                return Ok(user);
            }
            return NotFound("Usuário não encontrado");
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("O modelo recebido não é válido");
            }

            var test = await context.Users.AsNoTracking().FirstOrDefaultAsync(
                element => element.Username.Equals(model.Username));
          
            if(test != null) return BadRequest("Usuário já cadastrado");

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
                return BadRequest("Erro ao escrever no banco!");
            }
            
        }
        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromServices] AppDbContext context, 
            [FromBody] CreateUserViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("O modelo recebido não é válido");
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound("Usuário não encontrado");
            
            try
            {
                if(model.Username != "" && model.Username != null) user.Username = model.Username;
                if(model.Password != "" && model.Password != null) user.Password = model.Password;
                if(model.Role != "" && model.Role != null) user.Role = model.Role;
                if(model.UrlImagem != "" && model.UrlImagem != null) user.UrlImagem= model.UrlImagem;
                
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao acessar o banco");
            }
            
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound("Usuário não encontrado");
            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
               return BadRequest("Erro ao acessar o banco");
            }
            
        }
        
    }    
}
