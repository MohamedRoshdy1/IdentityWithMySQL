using FluentValidation.AspNetCore;
using IdenittyWithMySql;
using Identity.API.Extensions;
using Identity.API.Services;
using Identity.API.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
   builder.Services.AddFluentValidation(options =>
     {
         // Validate child properties and root collection elements
         options.ImplicitlyValidateChildProperties = true;
         options.ImplicitlyValidateRootCollectionElements = true;

         // Automatic registration of validators in assembly
         options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
     });
//.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductsValidators>());

//.AddFluentValidation(x=>x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddTransient<AuthManager>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
