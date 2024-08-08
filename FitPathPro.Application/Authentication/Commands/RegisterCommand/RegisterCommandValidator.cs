using FluentValidation;

namespace FitPathPro.Application.Authentication.Commands.RegisterCommand;

/// <summary>
/// Validates the input data to register a user
/// </summary>
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.input.FirstName)
            .MinimumLength(3)
            .MaximumLength(50)
            .OverridePropertyName("Name");

        RuleFor(x => x.input.LastName)
            .MinimumLength(3)
            .MaximumLength(100)
            .OverridePropertyName("Surname");

        RuleFor(x => x.input.Email)
            .NotEmpty()
            .EmailAddress()
            .OverridePropertyName("Email");
    }
}