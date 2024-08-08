using EduPrime.Api.Controllers;
using FitPathPro.Api.Response;
using FitPathPro.Application.Authentication.Commands.RegisterCommand;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Authentication.Queries;
using FitPathPro.Application.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitPathPro.Controllers;

public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("~/api/v1/users/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterUserDTO input)
    {
        var command = new RegisterCommand(input);
        var result = await _mediator.Send(command);

        Func<AuthenticationReponse, IActionResult> response = (authResponse) =>
            Ok(new ApiResponse<AuthenticationReponse>(authResponse));

        return result.Match(
            response,
            Problem
        );
    }

    [HttpPost("~/api/v1/users/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginUserDTO input)
    {
        var command = new LoginQuery(input);
        var result = await _mediator.Send(command);

        Func<AuthenticationReponse, IActionResult> response = (authResponse) =>
            Ok(new ApiResponse<AuthenticationReponse>(authResponse));

        return result.Match(
            response,
            Problem
        );
    }
}