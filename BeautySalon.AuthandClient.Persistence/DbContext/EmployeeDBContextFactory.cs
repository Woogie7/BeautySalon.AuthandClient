using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeautySalon.AuthandClient.Persistence;

public class AuthandClientDbContextFactory : IDesignTimeDbContextFactory<AuthandClientDbContext>
{
    public AuthandClientDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AuthandClientDbContext>();
        optionsBuilder.UseNpgsql("Host=authandclient-service-postgres;Port=5432;Database=BeautySalonAuthandClientdb;Username=postgres;Password=1234");

        return new AuthandClientDbContext(optionsBuilder.Options);
    }
}