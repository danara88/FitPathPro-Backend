using AutoMapper;
using FitPathPro.Application.Users.DTOs;
using FitPathPro.Domain.Users;

namespace FitPathPro.Application.Common.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User
        CreateMap<RegisterUserDTO, User>();
        CreateMap<User, UserDTO>();
        #endregion
    }
}