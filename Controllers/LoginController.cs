using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_API.ContextClass;
using UI_API.JwtService;
using UI_API.Models;

namespace UI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly AppDbContextClass context;
        public LoginController(AppDbContextClass context, IConfiguration configuration)
        {
            this.context = context;
            this._configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee(Register emp)
        {
            var existingEmployee = await context.Registered_User.FirstOrDefaultAsync(x => x.Email == emp.Email);
            if (existingEmployee != null)
            {
                return Ok("Email Already Exists");
            }

            emp.CreatedOn = DateTime.Now;
            context.Registered_User.Add(emp);
            await context.SaveChangesAsync();
            return Ok("Success");
        }

        [HttpPost("LoginEmployee")]
        public IActionResult LoginEmployee(Login login)
        {
            var employeeAvailable = context.Registered_User.Where(x => x.Email == login.Email && x.Pwd == login.Pwd).FirstOrDefault();
            if (employeeAvailable != null)
            {
                return Ok(new JwtClass(_configuration).GenerateToken(

                    employeeAvailable.Id.ToString(),
                    employeeAvailable.Name,
                    employeeAvailable.Email,
                    employeeAvailable.CreatedOn.ToString()
                    ));
            }
            return Ok("Failure");
        }
    }
}
