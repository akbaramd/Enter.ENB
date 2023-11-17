using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Roles;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Example.Api.Controllers;

[Route("api/jwt")]
public class JwtController : EntControllerBase
{
    public JwtController(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
    public IEntIdentityJwtAppService CrudService 
        => LazyServiceProvider.GetRequiredService<IEntIdentityJwtAppService>();

    [HttpPost("login")]
    public async Task<EntJwtResultDto> Create([FromBody] EntJwtLoginDto input)
    {
        return await CrudService.LoginAsync(input);
    }
    
    [HttpPost("refresh-token")]
    public async Task<EntJwtResultDto> RefreshToken([FromBody] EntJwtRefreshTokenDto input)
    {
        return await CrudService.RefreshTokenAsync(input);
    }
}