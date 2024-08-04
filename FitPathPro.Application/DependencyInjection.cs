using FitPathPro.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FitPathPro.Application;

/// <summary>
/// Dependency Injection Module for Application Layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(options => 
        {
            // Add commands/queries from the current assembly (Application layer)
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

            // Add generic behavior for validation
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Register all the validators IValidator<T> from the current assembly (Application layer)
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}