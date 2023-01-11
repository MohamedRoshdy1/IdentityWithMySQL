//using IdenittyWithMySql.Areas.CP.Models;
//using IdenittyWithMySql.Areas.Identity.Pages.Account;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace IdenittyWithMySql.Areas.CP.Controllers
//{
//    [Area("CP")]
//    public class RegistrationController : Controller
//    {
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly ILogger<RegistrationController> _logger;
//        private readonly ApplicationDbContext _context;

//        public RegistrationController(
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager,
//            ILogger<RegistrationController> logger, ApplicationDbContext context)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _logger = logger;
//            _context = context;
//        }
//        public IActionResult Index()
//        {
//            var users = _context.Users.ToList();
//            return View(users);
//        }
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Register([FromForm] InputModelForRegister InputRegister)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser { UserName = InputRegister.Email, Email = InputRegister.Email,FullName =InputRegister.FullName,PhoneNumber = InputRegister.PhoneNumber };
//                var result = await _userManager.CreateAsync(user, InputRegister.Password);
//                if (result.Succeeded)
//                {
//                    _logger.LogInformation("User created a new account with password.");

//                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
//                    {
//                        return View();
//                    }
//                    else
//                    {
//                        await _signInManager.SignInAsync(user, isPersistent: false);
//                        return RedirectToAction(nameof(Index), "Registration");

//                    }
//                }
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }

//            return View();
//        }
       
//    }
//}
