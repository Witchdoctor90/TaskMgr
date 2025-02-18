using Microsoft.Extensions.DependencyInjection;
using TaskMgr.Infrastructure.DB;

namespace TaskMgr.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddDbContext<ApplicationDbContext>()
            .AddAuthentication().Services;
    }
}