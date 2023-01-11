namespace Identity.API.DTOs
{
    public class UserRolesVM
    {
        public string UserId { get; set; }
        public List<RoleVM> Roles { get; set; }
    }
}
