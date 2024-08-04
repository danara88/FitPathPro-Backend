using AutoMapper;
using FitPathPro.Domain.Users;
using FitPathPro.Domain.Users.DTOs;

namespace FitPathPro.Application.Common.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User
        CreateMap<RegisterUserDTO, User>();
        #endregion
    }
}