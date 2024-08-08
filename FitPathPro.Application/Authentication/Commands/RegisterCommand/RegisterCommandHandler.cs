using AutoMapper;
using EduPrime.Core.Exceptions;
using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Application.Users.DTOs;
using FitPathPro.Domain.Users;
using MediatR;

namespace FitPathPro.Application.Authentication.Commands.RegisterCommand;

/// <summary>
/// Represents the register command handler
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationReponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtFactory _jwtFactory;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtFactory jwtFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtFactory = jwtFactory;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AuthenticationReponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.ExistsByEmailAsync(request.input.Email))
        {
            return AuthenticationErrors.UserAlreadyRegistered;
        }

        var hashPasswordResult = _passwordHasher.HashPassword(request.input.Password);
        if(hashPasswordResult.IsError)
        {
            return hashPasswordResult.Errors;
        }

        var user = new User
        {
            FirstName = request.input.FirstName,
            LastName = request.input.LastName,
            Email = request.input.Email,
            PasswordHash = hashPasswordResult.Value,
            LastLogin = DateTime.UtcNow
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch(Exception)
        {
            // TODO: Integrate Logger here to catch the error and store it
            throw new InternalServerException("User registration failed. Please try again.");
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        var accessToken = _jwtFactory.GenerateJwtToken(userDTO);

        return new AuthenticationReponse
        {
            AccessToken = accessToken,
        };
    }
}