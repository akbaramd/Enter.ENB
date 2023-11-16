using AutoMapper;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.Application.Contracts;

public class EntIdentityMapperProfile : Profile
{
    public EntIdentityMapperProfile()
    {
        CreateMap<EntIdentityUser, EntUserDto>();
        CreateMap<EntIdentityRole, EntRoleDto>();
    }
}