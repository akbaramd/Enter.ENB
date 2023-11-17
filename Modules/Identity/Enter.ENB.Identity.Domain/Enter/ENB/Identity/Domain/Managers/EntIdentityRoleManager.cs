using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Services;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.Enb.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Enter.ENB.Identity.Localization;

namespace Enter.ENB.Identity.Domain.Managers;

public class IdentityRoleManager :  IEntDomainService
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public IdentityRoleManager(IEntLazyServiceProvider lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }
    private IEntIdentityRoleRepository RoleRepository =>
        _lazyServiceProvider.LazyGetRequiredService<IEntIdentityRoleRepository>();
    private ICancellationTokenProvider CancellationTokenProvider =>
        _lazyServiceProvider.LazyGetRequiredService<ICancellationTokenProvider>();
    private CancellationToken CancellationToken => CancellationTokenProvider.Token;



    public async Task<List<EntIdentityRole>> Find()
    {
        return await RoleRepository.GetListAsync(false, CancellationToken);
    }
    
    public async Task<EntIdentityRole> FindByName(string name)
    {
        return await RoleRepository.GetAsync(x=>x.Name.Equals(name), cancellationToken: CancellationToken);
    }
    
}