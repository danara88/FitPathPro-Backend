namespace FitPathPro.Domain.Exercises;

public class Exercise 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Muscle { get; set; }

    public string? VideoUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
