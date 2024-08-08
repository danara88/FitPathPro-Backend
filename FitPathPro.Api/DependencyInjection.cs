
using System.Text;
using EduPrime.Infrastructure.Filters;
using FitPathPro.Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FitPathPro.Api;

/// <summary>
/// Dependeny Injection module for API layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => {
            options.Filters.Add<GlobalExceptionFilter>();
        }).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProblemDetails();

        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            byte[] secretKey = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSettings:Secret").Value!);

            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                // Always validate the secret key that is on the token
                ValidateIssuerSigningKey = true,

                // The key that we get must be equal to the key that we have created in the issuer
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                // Change to true in production envs.
                // It validates that the ORIGINAL issuer is the one that emit the token.
                // Ensures that there is not another source that emit the token.
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],

                // Change to true in production envs.
                // If the client (audience) receive the token, that client can not re-use it in any other place.
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],

                // Sets token expiration time
                RequireExpirationTime = true,

                // Validates the life time of the token
                ValidateLifetime = true
            };
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddCors(options => options.AddPolicy("AppCorsPolicy", build => 
        {
            build
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        return services;
    }
}