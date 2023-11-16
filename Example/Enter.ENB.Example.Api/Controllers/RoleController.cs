using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Example.Api.Controllers;


[Route("Api/Roles")]
public class RolesController : EntReadOnlyControllerBase<EntRoleDto,Guid>
{
    public RolesController(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}