using System.ComponentModel.DataAnnotations;

namespace UI_API.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}
