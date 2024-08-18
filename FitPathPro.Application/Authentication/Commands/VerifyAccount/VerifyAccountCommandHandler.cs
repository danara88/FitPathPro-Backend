using EduPrime.Core.Exceptions;
using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Common.Interfaces;
using MediatR;

namespace FitPathPro.Application.Authentication.Commands.VerifyAccount;

/// <summary>
/// Represents the verification account cammand handler
/// </summary>
public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, ErrorOr<Success>>
{
    private readonly IUnitOfWork _unitOfWork;

    public VerifyAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByVerificationTokenAsync(request.input);

        if (user is null || user.VerificationTokenExpires < DateTime.UtcNow)
        {
            return AuthenticationErrors.InvalidVerificationToken;
        }

        user.VerifiedAt = DateTime.UtcNow;

        try {
            await _unitOfWork.SaveChangesAsync();
        }
        catch(Exception)
        {
            // TODO: Integrate Logger here to catch the error and store it
            throw new InternalServerException("Account verification failed due to a server error. Please try again later.");
        }

        return Result.Success;
    }
}