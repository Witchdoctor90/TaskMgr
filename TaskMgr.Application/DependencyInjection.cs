using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaskMgr.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services )
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}