using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Configurations.ConfigureService;

public static class ServicesDi
{
    public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("Default")));

        service.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        service.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]!)),
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidateIssuer = true,
                ValidAudience = config["JWT:Audience"],
                ValidateAudience = true,
                RoleClaimType = ClaimTypes.Role
            };
        });

        service.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        service.AddAuthorization();
        
        return service;
    }
}