using IdenittyWithMySql.Areas.CP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdenittyWithMySql.Areas.CP.Controllers
{
    [Area("CP")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var Response = await _context.Orders.ToListAsync();
                return View(Response);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Orders model)
        {
            try
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;

                _context.Orders.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

    }
}
