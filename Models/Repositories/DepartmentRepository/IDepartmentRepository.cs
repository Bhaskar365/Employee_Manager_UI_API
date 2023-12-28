namespace UI_API.Models.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int depID);
        Task<Department> AddDepartment(Department dep);
    }
}
