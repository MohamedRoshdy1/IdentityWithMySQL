using IdenittyWithMySql.Areas.CP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace IdenittyWithMySql.Areas.CP.Controllers
{
    [Area("CP")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string roleName)
        {
            var model = new EditRoleViewModel();
            model.RoleName =   _roleManager.FindByNameAsync(roleName).Result.Name;
            model.RoleId = _roleManager.FindByNameAsync(roleName).Result.Id;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleToEdit =await _roleManager.FindByIdAsync(model.RoleId);

                
                if (roleToEdit.Name != model.RoleName)
                    roleToEdit.Name = model.RoleName;

                var result =await _roleManager.UpdateAsync(roleToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string roleName)
        {
            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByNameAsync(roleName);
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }

            return View(roleName);
        }

    }
}
