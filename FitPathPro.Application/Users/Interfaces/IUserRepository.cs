using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Domain.Users;

namespace FitPathPro.Application.Users.Interfaces;

/// <summary>
/// Represents the user repository interface
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmailAsync(string email);

    Task<bool> ExistsByEmailAsync(string email);

    Task<User> GetByVerificationTokenAsync(string verificationToken);
}