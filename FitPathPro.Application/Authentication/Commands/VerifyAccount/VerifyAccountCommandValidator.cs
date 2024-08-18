using FitPathPro.Application.Authentication.Commands.VerifyAccount;
using FluentValidation;

namespace FitPathPro.Application.Authentication.Commands.RegisterCommand;

/// <summary>
/// Validates the input data to verify an account
/// </summary>
public class VerifyAccountCommandValidator : AbstractValidator<VerifyAccountCommand>
{
    public VerifyAccountCommandValidator()
    {
        RuleFor(x => x.input)
            .NotEmpty()
            .OverridePropertyName("Token");
    }
}