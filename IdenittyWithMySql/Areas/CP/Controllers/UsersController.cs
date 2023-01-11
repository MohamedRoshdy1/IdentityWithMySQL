
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdenittyWithMySql.Areas.CP.Models;

namespace IdenittyWithMySql.Areas.CP.Controllers
{
    [Area("CP")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Result = await _context.Users.ToListAsync();
            return View(Result);
        }
        public async Task<IActionResult> MangeRoles(string userId)
        {
            var _user = await _userManager.FindByIdAsync(userId);
            if (_user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var vm = new UserRolesVM
            {
                UserName = _user.UserName,
                UserId = _user.Id,
                Roles = roles.Select(r => new RoleVM
                {
                    IsSelected = _userManager.IsInRoleAsync(_user, r.Name).Result,
                    RoleName = r.Name,
                    RoleId = r.Id,

                }).ToList()
            };
            return View(vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MangeRoles(UserRolesVM vm)
        {
            var _user = await _userManager.FindByIdAsync(vm.UserId);
            if (_user == null)
                return NotFound();
            var userRole = await _userManager.GetRolesAsync(_user);
            foreach (var role in vm.Roles)
            {
                if (userRole.Any(r => r == role.RoleName) && role.IsSelected == false)
                    await _userManager.RemoveFromRoleAsync(_user, role.RoleName);

                if (!userRole.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(_user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
