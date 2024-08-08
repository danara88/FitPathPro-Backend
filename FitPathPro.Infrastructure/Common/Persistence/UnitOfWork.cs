using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Application.Users.Interfaces;
using FitPathPro.Infrastructure.Users.Persistence;

namespace FitPathPro.Infrastructure.Common.Persistence;

/// <summary>
/// Represents unit of work implementation
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository = null;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
