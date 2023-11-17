using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Roles;

public interface IEntIdentityRoleAppService: IReadOnlyAppService<EntRoleDto,Guid> 
{
}