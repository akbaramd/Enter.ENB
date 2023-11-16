using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Identity.Application.Contracts.Roles;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;



[ExposeServices(typeof(IEntRoleAppService))]
public class RoleAppService :IdentityAppServiceBase, IEntRoleAppService ,ITransientDependency
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public RoleAppService(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }

    public IEntIdentityRoleRepository Repository => _lazyServiceProvider.GetRequiredService<IEntIdentityRoleRepository>();
    
    public async Task<EntRoleDto> GetAsync(Guid id)
    {
        var user = await Repository.GetAsync(id);
        return ObjectMapper.Map<EntIdentityRole,EntRoleDto>(user);
    }

    public async Task<PagedResultDto<EntRoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var list = await Repository.GetPagedListAsync(input.SkipCount,input.MaxResultCount,input.Sorting);
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<EntRoleDto>(
            totalCount,
            ObjectMapper.Map<List<EntIdentityRole>, List<EntRoleDto>>(list)
        );
    }

}