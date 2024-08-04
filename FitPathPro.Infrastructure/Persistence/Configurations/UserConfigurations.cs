using FitPathPro.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitPathPro.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(prop => prop.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(prop => prop.Surname)
            .HasMaxLength(100)
            .IsRequired();
    }
}