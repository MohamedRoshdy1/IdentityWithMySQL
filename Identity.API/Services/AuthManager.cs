
using IdenittyWithMySql;
using Identity.API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Services;
public class AuthManager
{
    
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly IConfiguration _configuration;
    private ApplicationUser _user;
    public AuthManager(UserManager<ApplicationUser> userManger, IConfiguration configuration)
    {
        _userManger = userManger;
        _configuration = configuration;

    }
   

    public async Task<string> CreateToken()
    {
        var signingCredational = GetSigningCredaionals();
        var claims = await GetClaims();
        var tokenOption = GenerateTokenOptions(signingCredational, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOption);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredational, List<Claim> claims)
    {
        var jwtSetting = _configuration.GetSection("Jwt");

        var option = new JwtSecurityToken(
            issuer: jwtSetting.GetSection("Issure").Value,
            claims: claims,
            expires: DateTime.Now.AddSeconds(Convert.ToDouble(jwtSetting.GetSection("lifetime").Value)),
            signingCredentials: signingCredational
            );

        return option;

    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,_user.UserName)
        };
        var roles = await _userManger.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;

    }

    private SigningCredentials GetSigningCredaionals()
    {
        var key = Environment.GetEnvironmentVariable("KEY");
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUser(LoginUserDTO userDto)
    {
        _user = await _userManger.FindByNameAsync(userDto.Email);

        return (_user != null && await _userManger.CheckPasswordAsync(_user, userDto.Password));

    }
}
