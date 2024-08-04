using ErrorOr;
using FitPathPro.Domain.Users.DTOs;
using MediatR;

namespace FitPathPro.Application.Users.Commands.RegisterCommand;

public record RegisterCommand(RegisterUserDTO input) : IRequest<ErrorOr<Success>> {}