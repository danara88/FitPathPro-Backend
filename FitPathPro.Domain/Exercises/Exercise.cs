using FitPathPro.Domain.Common.BaseEntity;

namespace FitPathPro.Domain.Exercises;

/// <summary>
/// Domain entity representing an Exercise
/// </summary>
public class Exercise : BaseEntity
{
    /// <summary>
    /// Represents the exercise name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Represents the muscle that the exercise impact
    /// </summary>
    public string Muscle { get; set; } = string.Empty;

    /// <summary>
    /// Represents the video of the exercise
    /// </summary>
    public string? VideoUrl { get; set; }
}
