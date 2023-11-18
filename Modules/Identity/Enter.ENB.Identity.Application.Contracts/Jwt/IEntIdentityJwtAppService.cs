using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts.Jwt.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Jwt;

public interface IEntIdentityJwtAppService: IApplicationService
{
    public Task<EntJwtResultDto> LoginAsync(EntJwtLoginDto input);
    public Task<EntJwtResultDto> RefreshTokenAsync(EntJwtRefreshTokenDto input);

}