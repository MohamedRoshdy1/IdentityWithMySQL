using IdenittyWithMySql.Areas.CP.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdenittyWithMySql.Areas.CP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthManager _authManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,AuthManager authManager)
        {
            _context = context;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Response = await _context.Products.ToListAsync();
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
      
        [HttpPost]
        public IActionResult Post(Products model)
        {
            try
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;

                _context.Products.Add(model);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
       
    }
}
