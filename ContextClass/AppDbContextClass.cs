using Microsoft.EntityFrameworkCore;
using UI_API.Models;

namespace UI_API.ContextClass
{
    public class AppDbContextClass : DbContext
    {
        public AppDbContextClass(DbContextOptions<AppDbContextClass> options):base(options) { }

        public DbSet<Employee> Employee_Table { get; set; }
        public DbSet<Department> Department_Table { get; set; }
        public DbSet<Register> Registered_User { get; set; }
    }
}
