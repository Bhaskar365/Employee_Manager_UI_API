using System.ComponentModel.DataAnnotations;

namespace UI_API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}
