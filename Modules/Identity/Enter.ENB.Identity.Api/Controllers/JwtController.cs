using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Jwt;
using Enter.ENB.Identity.Application.Contracts.Jwt.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Identity.Api.Controllers;

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