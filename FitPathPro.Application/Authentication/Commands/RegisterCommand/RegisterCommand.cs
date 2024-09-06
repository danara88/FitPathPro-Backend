using ErrorOr;
using FitPathPro.Application.Users.DTOs;
using MediatR;

namespace FitPathPro.Application.Authentication.Commands.RegisterCommand;

public record RegisterCommand(RegisterUserDTO input) : IRequest<ErrorOr<string>> {}