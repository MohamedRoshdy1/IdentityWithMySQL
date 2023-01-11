using AutoMapper;
using IdenittyWithMySql;
using Identity.API.DTOs;
using Identity.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Identity.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{


    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly AuthManager _authManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        ILogger<AccountController> logger,
        IMapper mapper,
        AuthManager authManager
        )
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
        _authManager = authManager;
    }



    [HttpPost]

    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] userDTO userDTO)
    {
        //Logger
        _logger.LogInformation($"Register Attemp For {userDTO.Email}");

        //check Model State
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = _mapper.Map<ApplicationUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userDTO.Roles);
            return Accepted();

        }
        catch (Exception ex)
        {
            //Logger Error
            _logger.LogError(ex, $"This is and error in {nameof(Register)} section");
            return Problem($"This is and error in {nameof(Register)} section", statusCode: 500);
            //return StatusCode(500, "his is and error in {nameof(Register)} section");
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
    {
        //Logger
        _logger.LogInformation($"Login Attemp For {userDTO.Email}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {

            if (await _authManager.ValidateUser(userDTO) == false)
            {
                return Unauthorized();
            }

            return Accepted(new { token = await _authManager.CreateToken() });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"This is and error in {nameof(Login)} section");
            return Problem($"This is and error in {nameof(Login)} section", statusCode: 500);
        }
    }
    [HttpPost("MangeRoles")]
    public async Task<IActionResult> MangeRoles([FromBody] UserRolesVM vm)
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
        return Ok("done");
    }




}
