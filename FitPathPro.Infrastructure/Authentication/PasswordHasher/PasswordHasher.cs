using System.Text.RegularExpressions;
using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Common.Interfaces;

namespace FitPathPro.Infrastructure.Authentication.PasswordHasher;

/// <summary>
/// Password hasher implementation
/// </summary>
public partial class PasswordHasher : IPasswordHasher
{
    private static readonly Regex PasswordRegex = StrongPasswordRegex();

    /// <summary>
    /// Method to hash a password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public ErrorOr<string> HashPassword(string password)
    {
        return !PasswordRegex.IsMatch(password)
            ? AuthenticationErrors.WeakPassword
            : BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    /// <summary>
    /// Method to compare if the hashed password is equal to the input password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    public bool IsCorrectPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }

    /// <summary>
    /// Gets a strong password regex
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();
}