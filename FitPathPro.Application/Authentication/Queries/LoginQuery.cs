using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Users.DTOs;
using MediatR;

namespace FitPathPro.Application.Authentication.Queries;

public record LoginQuery(LoginUserDTO input) : IRequest<ErrorOr<AuthenticationReponse>> {}