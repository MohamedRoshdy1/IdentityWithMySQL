using System.ComponentModel.DataAnnotations;

namespace IdenittyWithMySql.Areas.CP.Models
{
    public class EditRoleViewModel
    {
        [Required]
        public string RoleId { get; set; }

        [Display(Name = "Role name")]
        [Required]
        public string RoleName { get; set; }
    }
}
