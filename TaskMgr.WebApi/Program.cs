using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using TaskMgr.Application;
using TaskMgr.Application.Mappings;
using TaskMgr.Infrastructure;
using TaskMgr.Infrastructure.DB;
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
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        builder.Logging.AddAzureWebAppDiagnostics();

        builder.Services.Configure<AzureFileLoggerOptions>(options =>
        {
            options.FileName = "azure-diagnostics-";
            options.FileSizeLimit = 50 * 1024; // 50KB
            options.RetainedFileCountLimit = 5; // зберігати 5 файлів
        });

        services.AddLogging((lbuilder) => lbuilder
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole());

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
        
        app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();
        app.UseCors("AllowAll");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }
        
        app.Run();
    }
}