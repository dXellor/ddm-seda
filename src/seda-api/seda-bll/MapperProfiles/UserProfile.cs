using AutoMapper;
using seda_bll.Dtos.Auth;
using seda_bll.Dtos.Users;
using seda_dll.Models;

namespace seda_bll.MapperProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, RegistrationDto>().ReverseMap();
    }
}