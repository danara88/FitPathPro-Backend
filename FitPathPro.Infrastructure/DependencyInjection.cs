using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Infrastructure.Authentication.PasswordHasher;
using FitPathPro.Infrastructure.Authentication.TokenGenerator;
using FitPathPro.Infrastructure.Common.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitPathPro.Infrastructure;

/// <summary>
/// Dependency Injection module for infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // JWT configuration set up
        services.AddTransient<IJwtFactory, JwtFactory>();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}

