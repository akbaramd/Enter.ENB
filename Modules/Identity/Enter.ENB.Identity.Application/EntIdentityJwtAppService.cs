using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Exceptions;
using Enter.ENB.Identity.Application.Contracts.Roles;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[ExposeServices(
    typeof(IEntIdentityJwtAppService)
    )]
public class EntIdentityJwtAppService :EntIdentityAppServiceBase, IEntIdentityJwtAppService
{
    public IdentityUserManager UserManager => LazyServiceProvider.GetRequiredService<IdentityUserManager>();

    public async Task<EntJwtResultDto> LoginAsync(EntJwtLoginDto input)
    {
        var user = await UserManager.FindByUserNameAsync(input.UserName);

        if (user== null)
        {
            throw new EntException("Username/Password is Incorrect");
        }
        
        if (!user.ComparePassword(input.Password))
        {
            throw new EntException("Username/Password is Incorrect");
        }

        return new EntJwtResultDto()
        {
            AccessToken = user.Id.ToString(),
            RefreshToken = user.UserName,
            ExpireAt = DateTime.Now,
            ExpireAtUtc = DateTime.UtcNow
        };

    }

    public async Task<EntJwtResultDto> RefreshTokenAsync(EntJwtRefreshTokenDto input)
    {
        var user = await UserManager.GetByRefreshTokenAsync(input.RefreshToken);
        
        user.NewRefreshToken();
        
        return new EntJwtResultDto()
        {
            AccessToken = user.Id.ToString(),
            RefreshToken = user.UserName,
            ExpireAt = DateTime.Now,
            ExpireAtUtc = DateTime.UtcNow
        };
    }
}