using System.Text.Encodings.Web;
using EduPrime.Core.Exceptions;
using ErrorOr;
using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FitPathPro.Application.Authentication.Commands.SendVerificationEmailCommand;

public class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    private readonly IWebHostEnvironment _hostEnvironment;

    public SendVerificationEmailCommandHandler(
        IUnitOfWork unitOfWork, 
        IEmailSender emailSender, 
        IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<ErrorOr<string>> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.input.Email);

        if(user is null || user.VerifiedAt is not null)
        {
            return Error.Validation(description: "Ups! Something went wrong while sending verification email.");
        }

        try
        {
            await SendVerificationEmail(user);
        }
        catch(Exception)
        {
            throw new InternalServerException("Oops! Something went wrong.");
        }

        return "Verification email sent";
    }

    private async Task SendVerificationEmail(User user)
    {
        // Example: https://localhost:44392/api/users/v1/confirm-email?code=exampleCode
        // var callbackUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}"
        //                     + $"/api/v1/auth/verify-account?token={user.VerificationToken}";

        var callbackUrl = $"http://localhost:4200/auth/confirm-account?token={user.VerificationToken}";

        var pathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
            + "Templates" 
            + Path.DirectorySeparatorChar.ToString() 
            + "VerifyEmailTemplate" 
            + Path.DirectorySeparatorChar.ToString() 
            + "Verify_Email_Account.html";

        var htmlBody = "";
        using (StreamReader streamReader = File.OpenText(pathToFile))
        {
            htmlBody = streamReader.ReadToEnd();
        }

        string callbackUrlItem = $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style=\"font-size: 1rem;padding: 1rem;background-color: #5b76e7;color: white;text-decoration: none;border-radius: 0.4rem;margin: 1rem 0rem;display: inline-block;font-weight: bold;\">Confirm Email Address</a>";

        string messageBody = string.Format(htmlBody, user.Email, callbackUrlItem);

        try
        {
            await _emailSender.SendEmailAsync(user.Email, "FitPathPro Inc. Confirm your email.", messageBody);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

}

