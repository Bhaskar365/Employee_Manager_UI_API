using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_API.ContextClass;
using UI_API.Models;
using UI_API.Models.Repositories.DepartmentRepository;

namespace UI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        public readonly AppDbContextClass dataContext;
        public readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(AppDbContextClass dataContext, IDepartmentRepository departmentRepository)
        {
            this.dataContext = dataContext;
            this.departmentRepository = departmentRepository;
        }

        [HttpGet("allDepartments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            try
            {
                return Ok(await departmentRepository.GetDepartments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                var result = await departmentRepository.GetDepartment(id);

                if (result == null) 
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }           
        }

        [HttpPost("NewDepartment")]
        public async Task<ActionResult<Department>> AddNewDepartment([FromBody] Department dep) 
        {
            try
            {
                if (dep==null) 
                {
                    return NotFound();
                }

                var createdDepartment = await departmentRepository.AddDepartment(dep);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        
    }
}
