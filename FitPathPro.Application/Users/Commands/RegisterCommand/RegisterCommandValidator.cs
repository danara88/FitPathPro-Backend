using FluentValidation;

namespace FitPathPro.Application.Users.Commands.RegisterCommand;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.input.Name)
            .MinimumLength(3)
            .MaximumLength(50)
            .OverridePropertyName("UserName");

        RuleFor(x => x.input.Surname)
            .MinimumLength(3)
            .MaximumLength(100)
            .OverridePropertyName("UserSurname");

        RuleFor(x => x.input.Email)
            .NotEmpty()
            .EmailAddress()
            .OverridePropertyName("UserEmail");
    }
}