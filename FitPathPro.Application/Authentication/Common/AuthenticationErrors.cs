using ErrorOr;

namespace FitPathPro.Application.Authentication.Common;

/// <summary>
/// Authentication errors list
/// </summary>
public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = Error.Unauthorized(
        code: "Authentication.InvalidCredentials",
        description: "Your email account or password is incorrect.");

    public static readonly Error WeakPassword = Error.Validation(
        code: "Authentication.WeakPassword",
        description: "Password too weak. Password must be 8+ characters, with uppercase, lowercase, a number, and a special character.");

    public static readonly Error UserAlreadyRegistered = Error.Conflict(
        code: "Authentication.UserAlreadyRegistered",
        description: "This account already exists. Enter a different account or request a new one.");
}