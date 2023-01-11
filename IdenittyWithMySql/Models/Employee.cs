
using System.ComponentModel.DataAnnotations;

namespace IdenittyWithMySql.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
