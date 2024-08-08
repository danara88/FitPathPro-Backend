using FitPathPro.Application.Users.Interfaces;
using FitPathPro.Domain.Users;
using FitPathPro.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitPathPro.Infrastructure.Users.Persistence;

/// <summary>
/// Represents user repository methods to interact with database
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to check if a user exists by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _entity.AnyAsync(u => u.Email == email);
    }

    /// <summary>
    /// Method to find a user by email address
    /// </summary>
    /// <param name="email"></param>
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _entity.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Method to find a user by verification token 
    /// </summary>
    /// <param name="verificationToken"></param>
    public async Task<User> GetByVerificationTokenAsync(string verificationToken)
    {
        return await _entity.FirstOrDefaultAsync(u => u.VerificationToken == verificationToken);
    }
}