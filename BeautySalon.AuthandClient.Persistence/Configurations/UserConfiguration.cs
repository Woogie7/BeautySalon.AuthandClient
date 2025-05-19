using BeautySalon.AuthandClient.Domain;
using BeautySalon.AuthandClient.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.AuthandClient.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Role)
            .HasConversion(
                role => role.Name,
                name => Enumeration.FromDisplayName<UserRole>(name)
            )
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}