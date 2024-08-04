using Microsoft.AspNetCore.Identity;

namespace FitPathPro.Domain.Roles;

public class Role : IdentityRole<int>
{
    public string? Description { get; set; }
}