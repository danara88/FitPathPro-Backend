using EduPrime.Api.Controllers;
using FitPathPro.Application.Users.Commands.RegisterCommand;
using FitPathPro.Domain.Users.DTOs;
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

    [HttpPost("~/api/v1/users/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterUserDTO input)
    {
        var command = new RegisterCommand(input);
        var result = await _mediator.Send(command);

       return result.Match(_ => Ok(), Problem);
    }
}