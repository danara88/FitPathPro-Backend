namespace FitPathPro.Domain.Common.BaseEntity;

/// <summary>
/// Represents the base for all domain entities
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Entity primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Creation date of the domain entity
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Updated date of the domain entity
    /// </summary>
    public DateTime? UpdatedOn { get; set; }
}