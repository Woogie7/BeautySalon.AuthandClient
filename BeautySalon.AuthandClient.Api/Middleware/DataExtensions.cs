using BeautySalon.AuthandClient.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.AuthandClient.Middleware;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<AuthandClientDbContext>();
        await DbContext.Database.MigrateAsync();
    }
}