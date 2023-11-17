using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[DependsOnModules(typeof(EntIdentityApplicationContractsModule))]
public class EntIdentityApplicationModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // context.Services.AddTransient<ICrudAppService<EntUserDto,Guid,PagedAndSortedResultRequestDto,UserCreateDto,UserUpdateDto> , UserAppService>();
        // context.Services.AddTransient<IReadOnlyAppService<EntRoleDto,Guid>,RoleAppService>();
    }
}