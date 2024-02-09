using api.Data.Dtos.User;
using api.Models;
using AutoMapper;

namespace api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        _ = CreateMap<CreateUserDto, User>();
    }
}
