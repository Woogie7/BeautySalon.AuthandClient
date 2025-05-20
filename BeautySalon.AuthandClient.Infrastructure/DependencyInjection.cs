using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Infrastructure.Auht;
using BeautySalon.AuthandClient.Infrastructure.HasherPass;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.AuthandClient.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}