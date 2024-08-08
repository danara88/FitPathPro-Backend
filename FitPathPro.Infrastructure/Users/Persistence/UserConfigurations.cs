using FitPathPro.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitPathPro.Infrastructure.Users.Persistence;

/// <summary>
/// Sets user table configurations
/// </summary>
public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(prop => prop.FirstName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(prop => prop.LastName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(prop => prop.Email)
            .IsUnicode(false)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(prop => prop.PasswordHash)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(prop => prop.LastLogin)
            .IsRequired(false);

        builder.Property(prop => prop.VerificationToken)
            .IsUnicode(false)
            .IsRequired(false);

        builder.Property(prop => prop.VerifiedAt)
            .IsRequired(false);

        builder.Property(prop => prop.VerifiedAt)
            .IsRequired(false);

        builder.Property(prop => prop.PasswordResetToken)
            .IsUnicode(false)
            .IsRequired(false);
          
        builder.Property(prop => prop.PasswordResetTokenExpires)
            .IsRequired(false);

        builder.Property(prop => prop.CreatedOn)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }
}