using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application;
using Enter.ENB.Identity.Application.Contracts.Roles;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Example.Api.Controllers;


[Route("Api/Roles")]
public class RolesController : ControllerBase
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public RolesController(IEntLazyServiceProvider lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }

    public IEntIdentityRoleAppService IdentityRoleAppService => _lazyServiceProvider.LazyGetRequiredService<IEntIdentityRoleAppService>();
    
    [HttpGet("Get")]
    public async Task<PagedResultDto<EntRoleDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        return await IdentityRoleAppService.GetListAsync(input);
    }
    [HttpGet("Get/{id}")]
    public async Task<EntRoleDto> GetAsync(Guid id)
    {
        return await IdentityRoleAppService.GetAsync(id);
    }
}