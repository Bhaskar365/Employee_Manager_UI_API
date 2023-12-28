using Microsoft.EntityFrameworkCore;
using UI_API.ContextClass;

namespace UI_API.Models.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly AppDbContextClass context;
        public EmployeeRepository(AppDbContextClass context) 
        {
            this.context = context;
        }
        public async Task<Employee> AddEmployee(Employee empParams)
        {
            empParams.CreatedOn = DateTime.UtcNow.ToString();
            var result = await context.AddAsync(empParams);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var result = await context.Employee_Table.FirstOrDefaultAsync(id => id.UserID == employeeId);
            if(result!=null) 
            {
                context.Employee_Table.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await context.Employee_Table.ToListAsync();
        }

        public async Task<Employee> GetEmpById(int id)
        {
            return await context.Employee_Table.Include(e => e._Department.DepartmentId).FirstOrDefaultAsync(e => e.UserID == id);
        }

        public Task<Employee> GetEmployeeEmail(string email)
        {
            return context.Employee_Table.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> UpdateEmployee(int id, Employee empParams)
        {
            var result = await context.Employee_Table.FirstOrDefaultAsync(e => e.UserID == id);

            if (result != null)
            {
                result.FirstName = empParams.FirstName;
                result.LastName = empParams.LastName;
                result.DateOfHire = empParams.DateOfHire;
                result.Gender = empParams.Gender;
                result.Email = empParams.Email;
                result.Country = empParams.Country;
                result.City = empParams.City;
                result.State = empParams.State;
                result.ZipCode = empParams.ZipCode;
                result.Phone = empParams.Phone;
                result.Position = empParams.Position;
                result.userImage = empParams.userImage;
                result.CTC = empParams.CTC;
                result.DepartmentId = empParams.DepartmentId;

                await context.SaveChangesAsync();

                return result;
            }
            return null;
        }
        public bool CreateEmployee(int employeeId, Employee employee) 
        {
            var departmentEntity = context.Department_Table.Where(d => d.DepartmentId == employeeId);

            employee.CreatedOn = DateTime.UtcNow.ToString();

            context.Add(employee);

            var saved =  context.SaveChanges();

            return saved > 0 ? true : false;

        }

    }
}
