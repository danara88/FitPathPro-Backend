using EduPrime.Api.Controllers;
using FitPathPro.Api.Response;
using FitPathPro.Application.Authentication.Commands.RegisterCommand;
using FitPathPro.Application.Authentication.Commands.SendVerificationEmailCommand;
using FitPathPro.Application.Authentication.Commands.VerifyAccount;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Authentication.Queries;
using FitPathPro.Application.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitPathPro.Controllers;

public class AuthController : ApiController
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
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

        Func<string, IActionResult> response = (message) =>
            Ok(new ApiMessageResponse(message));

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

    [HttpPost("~/api/v1/auth/send-verification-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendVerificationEmail([FromBody] SendVerificationEmailDTO input)
    {
        var command = new SendVerificationEmailCommand(input);
        var result = await _mediator.Send(command);

        Func<string, IActionResult> response = (message) =>
            Ok(new ApiMessageResponse(message));

        return result.Match(
            response,
            Problem
        );
    }

    [HttpPost("~/api/v1/auth/verify-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyAccount([FromQuery] string token)
    {
        var command = new VerifyAccountCommand(token);
        var result = await _mediator.Send(command);

       Func<string, IActionResult> response = (message) =>
            Ok(new ApiMessageResponse(message));

        return result.Match(
            response,
            Problem
        );
    }
}