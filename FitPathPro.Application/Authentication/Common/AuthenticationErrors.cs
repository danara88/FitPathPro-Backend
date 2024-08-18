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

    public static readonly Error UserAccountNotVerified = Error.Validation(
        code: "Authentication.UserAccountNotVerified",
        description: "Your account is not verified yet. Please check your email inbox for the verification link and confirm your account."
    );

    public static readonly Error InvalidVerificationToken = Error.Validation(
        code: "Authentication.InvalidVerificationToken",
        description: "An error occurred while verifying your account."
    );
}