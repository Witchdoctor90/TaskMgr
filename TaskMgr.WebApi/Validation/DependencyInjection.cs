using FluentValidation;
using FluentValidation.AspNetCore;

namespace TaskMgr.WebApi.Validation;

public static class DependencyInjection
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<TaskEntityValidator>();
        return services;
    }
}