namespace UI_API.Models.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmpById(int id);
        Task<Employee> GetEmployeeEmail(string email);
        Task<Employee> AddEmployee(Employee empParams);
        Task<Employee> UpdateEmployee(int id, Employee empParams);
        Task<Employee> DeleteEmployee(int employeeId);
        bool CreateEmployee(int departmentId, Employee emp);
    }
}
