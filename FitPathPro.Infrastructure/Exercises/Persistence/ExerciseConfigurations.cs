using FitPathPro.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitPathPro.Infrastructure.Exercises.Persistence;

/// <summary>
/// Sets exercise table configurations
/// </summary
public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Muscle)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.VideoUrl)
            .IsUnicode(false)
            .IsRequired(false);
            
        builder.Property(e => e.CreatedOn)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }
}