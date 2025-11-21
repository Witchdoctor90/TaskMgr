using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskMgr.Application.Interfaces;
using TaskMgr.Infrastructure.DB;
using TaskMgr.Infrastructure.Identity;

namespace TaskMgr.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts
            => opts.UseNpgsql(configuration.GetConnectionString("LocalhostConnection")));

        services.AddIdentity<User, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.User.RequireUniqueEmail = true;
                opts.SignIn.RequireConfirmedEmail = false;
                opts.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}