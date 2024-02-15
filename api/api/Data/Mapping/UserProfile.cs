using api.Data.Dtos.User;
using api.Data.Models;
using AutoMapper;

namespace api.Data.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        _ = CreateMap<CreateUserDto, User>();
        _ = CreateMap<User, LoginUserDto>();
        _ = CreateMap<User, ReadUserDto>();
    }
}
