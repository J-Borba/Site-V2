using projects_api.Data.Dtos.User;
using projects_api.Data.Models;
using AutoMapper;

namespace projects_api.Data.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName ?? src.Email));

        CreateMap<User, ReadUserDto>();
    }
}
