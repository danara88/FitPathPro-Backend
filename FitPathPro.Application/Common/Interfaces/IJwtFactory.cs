using FitPathPro.Application.Users.DTOs;

namespace FitPathPro.Application.Common.Interfaces;

/// <summary>
/// JWT Factory interface
/// </summary>
public interface IJwtFactory
{
    string GenerateJwtToken(UserDTO userDTO);
}
