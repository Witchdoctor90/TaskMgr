using TaskMgr.Application;
using TaskMgr.Infrastructure;
using TaskMgr.WebApi.Validation;

namespace TaskMgr.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddFluentValidation();


        var app = builder.Build();

        app.Run();
    }
}