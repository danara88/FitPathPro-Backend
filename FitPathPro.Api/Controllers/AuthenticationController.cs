using EduPrime.Api.Controllers;
using ErrorOr;
using FitPathPro.Api.Response;
using FitPathPro.Application.Authentication.Commands.RegisterCommand;
using FitPathPro.Application.Authentication.Commands.VerifyAccount;
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

    [HttpPost("~/api/v1/auth/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO input)
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

    [HttpPost("~/api/v1/auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO input)
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

    [HttpGet("~/api/v1/auth/verify-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyAccount([FromQuery] string token)
    {
        var command = new VerifyAccountCommand(token);
        var result = await _mediator.Send(command);

        // TODO: Move this URL to consts file
        Func<Success, IActionResult> response = (result) =>
            Redirect("http://localhost:4200/auth/register");

        return result.Match(
            response,
            Problem
        );
    }
}