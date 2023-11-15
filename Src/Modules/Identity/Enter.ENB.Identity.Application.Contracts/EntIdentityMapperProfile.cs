using AutoMapper;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.Application;

public class EntIdentityMapperProfile : Profile
{
    public EntIdentityMapperProfile()
    {
        CreateMap<EntUser, UserDto>();
        CreateMap<CreateUpdateUserDto, EntUser>();
        CreateMap<UserDto, EntUser>();
    }
}