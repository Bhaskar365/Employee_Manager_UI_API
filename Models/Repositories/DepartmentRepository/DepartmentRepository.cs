using Microsoft.EntityFrameworkCore;
using UI_API.ContextClass;

namespace UI_API.Models.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly AppDbContextClass context;
        public DepartmentRepository(AppDbContextClass context)
        {
            this.context = context;
        }

        public async Task<Department> GetDepartment(int depID)
        {
            return await context.Department_Table.FirstOrDefaultAsync(d => d.DepartmentId == depID);
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await context.Department_Table.ToListAsync();
        }

        public async Task<Department> AddDepartment(Department dep)
        {
            var result = await context.Department_Table.AddAsync(dep);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
