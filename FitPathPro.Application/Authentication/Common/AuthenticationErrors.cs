using ErrorOr;

namespace FitPathPro.Application.Authentication.Common;

/// <summary>
/// Authentication errors list
/// </summary>
public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = Error.Unauthorized(
        code: "Authentication.InvalidCredentials",
        description: "Invalid credentials");

    public static readonly Error WeakPassword = Error.Validation(
        code: "Authentication.WeakPassword",
        description: "Password too weak");

    public static readonly Error UserAlreadyRegistered = Error.Conflict(
        code: "Authentication.UserAlreadyRegistered",
        description: "User already registered");
}