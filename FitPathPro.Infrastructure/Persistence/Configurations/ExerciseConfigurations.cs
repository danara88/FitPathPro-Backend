using FitPathPro.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitPathPro.Infrastructure.Persistence.Configurations;

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
            .IsUnicode(false);

        builder.Property(e => e.UpdatedOn)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
            
        builder.Property(e => e.CreatedOn)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }
}