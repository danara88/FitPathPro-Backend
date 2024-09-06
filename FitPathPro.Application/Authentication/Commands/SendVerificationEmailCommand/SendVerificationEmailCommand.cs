using ErrorOr;
using FitPathPro.Application.Users.DTOs;
using MediatR;

namespace FitPathPro.Application.Authentication.Commands.SendVerificationEmailCommand;

public record SendVerificationEmailCommand(SendVerificationEmailDTO input) : IRequest<ErrorOr<string>> {}