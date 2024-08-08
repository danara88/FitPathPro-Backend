using System.Reflection;
using FitPathPro.Domain.Exercises;
using FitPathPro.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FitPathPro.Infrastructure.Common.Persistence;

public class ApplicationDbContext : DbContext
{   
    /// <summary>
    /// Users table
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Exercises table
    /// </summary>
    public DbSet<Exercise> Exercises { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}