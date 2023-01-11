using IdenittyWithMySql;
using IdenittyWithMySql.Areas.CP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegistrationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] InputModelForRegister InputRegister)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = InputRegister.Email, Email = InputRegister.Email, FullName = InputRegister.FullName, PhoneNumber = InputRegister.PhoneNumber };
                var result = await _userManager.CreateAsync(user, InputRegister.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return Ok();
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok(new { user });

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return NotFound();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] InputModelForLogin InputLogin)
        {

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(InputLogin.Email, InputLogin.Password, InputLogin.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok("This User Is Login");
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Ok("locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest("Invalid login attempt.");
                }
            }

            return NotFound();
        }

    

    }
}
