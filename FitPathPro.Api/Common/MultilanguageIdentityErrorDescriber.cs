using Microsoft.AspNetCore.Identity;

namespace FitPathPro.Api;

/// <summary>
/// Override Identity error messages.
/// https://stackoverflow.com/questions/66908066/how-to-change-the-identity-error-for-the-register-page-in-razor-pages
/// </summary>
public class AppIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string username)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"Email '{username}' is already registered." 
        };
    }

     public override IdentityError DuplicateEmail(string email) 
     { 
        return new IdentityError 
        { 
            Code = nameof(DuplicateEmail),
            Description = $"Email '{email}' is already registered."  
        }; 
    }
}