using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI_API.ContextClass;
using UI_API.Models;
using UI_API.Models.Repositories.EmployeeRepository;

namespace UI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly AppDbContextClass context;
        public readonly IEmployeeRepository employeeRepository;
        public EmployeeController(AppDbContextClass context, IEmployeeRepository employeeRepository) 
        {
            this.context = context;
            this.employeeRepository = employeeRepository;
        }

        [HttpGet("employees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees() 
        {
            var result = await employeeRepository.GetAllEmployees();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id) 
        {
            var result = await employeeRepository.GetEmpById(id);
            if(result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        //[HttpPost("AddNew")]
        //public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee emp) 
        //{
        //    try
        //    {
        //        if (emp == null) 
        //        {
        //            return BadRequest();
        //        }

        //        var createdEmployee = await employeeRepository.AddEmployee(emp);
        //        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.UserID }, createdEmployee);

        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,"Error fetching data from server");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee emp) 
        {
            try
            {
                var employeeToUpdate = await employeeRepository.GetEmpById(id);

                if(employeeToUpdate == null)
                {
                    return NotFound($"Employee with id : {id} not found");
                }
                return await employeeRepository.UpdateEmployee(id,emp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching data from server");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id) 
        {
            try
            {
                var empToDelete = await employeeRepository.GetEmpById(id);

                if(empToDelete == null) 
                {
                    return NotFound($"Employee with id : {id} not found");
                }
                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching data from server");
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult CreateNewEmployee([FromQuery] int departmentId, [FromBody] Employee employee) 
        {
            if (employee == null)
                return BadRequest(ModelState);

            var department = context.Department_Table.Where(d => d.DepartmentId == employee._Department.DepartmentId).FirstOrDefault();
           
            if(department == null)
            {
                ModelState.AddModelError("","Department not selected");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!employeeRepository.CreateEmployee(departmentId, employee)) 
            {
                ModelState.AddModelError("","Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Added");
        }
    }
}
