using FluentValidation;

namespace FitPathPro.Application.Authentication.Commands.SendVerificationEmailCommand;

public class SendVerificationEmailCommandValidator : AbstractValidator<SendVerificationEmailCommand> {
    public SendVerificationEmailCommandValidator()
    {
        RuleFor(x => x.input.Email)
            .NotEmpty()
            .EmailAddress()
            .OverridePropertyName("Email");
    }
}