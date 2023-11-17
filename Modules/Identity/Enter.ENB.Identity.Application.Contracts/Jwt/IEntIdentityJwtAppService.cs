using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Roles;

public interface IEntIdentityJwtAppService: IApplicationService
{
    public Task<EntJwtResultDto> LoginAsync(EntJwtLoginDto input);
    public Task<EntJwtResultDto> RefreshTokenAsync(EntJwtRefreshTokenDto input);

}