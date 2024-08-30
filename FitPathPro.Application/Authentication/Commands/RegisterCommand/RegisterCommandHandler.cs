using System.Security.Cryptography;
using System.Text.Encodings.Web;
using EduPrime.Core.Exceptions;
using ErrorOr;
using FitPathPro.Application.Authentication.Common;
using FitPathPro.Application.Common.Interfaces;
using FitPathPro.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FitPathPro.Application.Authentication.Commands.RegisterCommand;

/// <summary>
/// Represents the register command handler
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IEmailSender _emailSender;

    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostEnvironment,
        IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _emailSender = emailSender;
    }

    public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
            VerificationToken = CreateRandomeToken(),
            VerificationTokenExpires = DateTime.UtcNow.AddDays(2),
            LastLogin = DateTime.UtcNow
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        
        try
        {
            await _unitOfWork.SaveChangesAsync();
            await SendVerificationEmail(user);
        }
        catch(Exception)
        {
            // TODO: Integrate Logger here to catch the error and store it
            throw new InternalServerException();
        }

        return $"Success! A verification email has been sent to {user.Email}. Please check your inbox and follow the instructions to complete your registration.";
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

    private string CreateRandomeToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}