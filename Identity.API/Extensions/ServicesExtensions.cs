using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
              .AddJwtBearer(op =>
              {
                  op.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateLifetime = true,
                      ValidateAudience = false,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtSetting.GetSection("Issure").Value,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                  };
              });

        }
       
    }
}
