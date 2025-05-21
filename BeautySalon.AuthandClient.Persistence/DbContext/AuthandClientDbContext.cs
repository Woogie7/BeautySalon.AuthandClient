using BeautySalon.AuthandClient.Domain;
using BeautySalon.AuthandClient.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace BeautySalon.AuthandClient.Persistence;

public class AuthandClientDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    public AuthandClientDbContext(DbContextOptions<AuthandClientDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthandClientDbContext).Assembly);
        SeedData(modelBuilder);
    }
    private void SeedData(ModelBuilder modelBuilder)
    {
        var userRoles = Enumeration.GetAll<UserRole>();

        modelBuilder.Entity<UserRole>().HasData(userRoles);
    }
}