using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Infrastructure.Auh;
using BeautySalon.AuthandClient.Infrastructure.Auht;
using BeautySalon.AuthandClient.Infrastructure.HasherPass;
using BeautySalon.AuthandClient.Infrastructure.Rabbitmq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.AuthandClient.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        
        services.AddTransient<IEventBus, EventBus>();
        return services;
    }
}