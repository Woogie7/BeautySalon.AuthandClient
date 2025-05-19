using BeautySalon.AuthandClient.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.AuthandClient.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Role).IsRequired();
        builder.HasOne(x => x.Client)
            .WithOne(c => c.User)
            .HasForeignKey<Client>(c => c.Id);
    }
}