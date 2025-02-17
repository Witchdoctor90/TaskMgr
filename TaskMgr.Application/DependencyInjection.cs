using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskMgr.Application.Mappings;

namespace TaskMgr.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
    }
}