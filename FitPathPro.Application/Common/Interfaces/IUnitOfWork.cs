using FitPathPro.Application.Users.Interfaces;

namespace FitPathPro.Application.Common.Interfaces;

/// <summary>
/// Unit of work interface
/// </summary>
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    Task SaveChangesAsync();
}