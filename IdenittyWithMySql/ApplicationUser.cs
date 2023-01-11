

using Microsoft.AspNetCore.Identity;

namespace IdenittyWithMySql
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
