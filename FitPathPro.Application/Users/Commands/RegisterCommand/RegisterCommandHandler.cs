using AutoMapper;
using EduPrime.Core.Exceptions;
using ErrorOr;
using FitPathPro.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitPathPro.Application.Users.Commands.RegisterCommand;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Success>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Success>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.input);

        // ASP NET Core Identity takes in consideration the username instead of email.
        // For now the system is supporting email and not username.
        // Pass the email though the username property to register the user.
        user.UserName = request.input.Email; 

        try
        {
            var result = await _userManager.CreateAsync(user, request.input.Password!);

            if(result.Errors.Count() > 0)
            {
                var errors = result.Errors
                    .ToList()
                    .ConvertAll(error => 
                        Error.Validation(code: error.Code, description: error.Description));

                return errors;
            }
        
            return Result.Success;
        }
        catch(Exception)
        {   
            // Log error info here.
            throw new InternalServerException("Something went wrong while registering.");
        }
    }
}