using TaskMgr.Application;
using TaskMgr.Application.Mappings;
using TaskMgr.Infrastructure;
using TaskMgr.WebApi.Interfaces;
using TaskMgr.WebApi.Mappings;
using TaskMgr.WebApi.Services;
using TaskMgr.WebApi.Validation;

namespace TaskMgr.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        services.AddLogging(builder => builder.AddConsole());

        services.AddApplication();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ApplicationMappingProfile>();
            cfg.AddProfile<WebApiMappingProfile>();
        });
        services.AddInfrastructure(builder.Configuration);
        services.AddControllers();
        services.AddFluentValidation();
        if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUsersService, UsersService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}