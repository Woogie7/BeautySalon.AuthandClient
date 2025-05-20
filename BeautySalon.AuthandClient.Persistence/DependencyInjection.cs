using BeautySalon.AuthandClient.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.AuthandClient.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration confing)
    {
        var сonnection = confing.GetConnectionString("AuthandClientDatabase");
        services.AddDbContext<AuthandClientDbContext>(o =>
        {
            o.UseNpgsql(сonnection);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}