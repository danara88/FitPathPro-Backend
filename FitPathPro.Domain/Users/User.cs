using FitPathPro.Domain.Common.BaseEntity;

namespace FitPathPro.Domain.Users;

/// <summary>
/// Domain entity representing a User
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// User's first name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// User's last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// User's email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's hashed password
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the user logged in
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Token for verifying the user's account
    /// </summary>
    public string? VerificationToken { get; set; }

    /// <summary>
    /// Date and time when the verification token expires
    /// </summary>
    public DateTime? VerificationTokenExpires { get; set; }

    /// <summary>
    /// Date and time when the user verified their account via email
    /// </summary>
    public DateTime? VerifiedAt { get; set; }

    /// <summary>
    /// Random token to allow the user to reset their password
    /// </summary>
    public string? PasswordResetToken { get; set; }

    /// <summary>
    /// Date and time when the password reset token expires
    /// </summary>
    public DateTime? PasswordResetTokenExpires { get; set; }
}