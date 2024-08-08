using AutoMapper;
using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Application.Users.DTOs;
using MediatR;

namespace FitPathPro.Application.Authentication.Queries;

/// <summary>
/// Login query handler in charge of processing the login logic
/// </summary>
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationReponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtFactory _jwtFactory;

    public LoginQueryHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IMapper mapper, IJwtFactory jwtFactory)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtFactory = jwtFactory;
    }


    public async Task<ErrorOr<AuthenticationReponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.input.Email);

        if (user is null || !_passwordHasher.IsCorrectPassword(request.input.Password, user.PasswordHash))
        {
            return AuthenticationErrors.InvalidCredentials;
        }

        var userDTO = _mapper.Map<UserDTO>(user);

        return new AuthenticationReponse
        {
            AccessToken = _jwtFactory.GenerateJwtToken(userDTO)
        };
    }
}