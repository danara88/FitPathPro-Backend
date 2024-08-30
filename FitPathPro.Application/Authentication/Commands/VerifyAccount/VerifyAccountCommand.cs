using ErrorOr;
using MediatR;

namespace FitPathPro.Application.Authentication.Commands.VerifyAccount;

public record VerifyAccountCommand(string input) : IRequest<ErrorOr<string>> {}