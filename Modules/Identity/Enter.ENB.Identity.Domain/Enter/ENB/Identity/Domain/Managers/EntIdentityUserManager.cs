using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Services;
using Enter.ENB.Exceptions;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.ENB.Statics;
using Enter.Enb.Threading;

namespace Enter.ENB.Identity;

public class IdentityUserManager : IEntDomainService
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public IdentityUserManager(IEntLazyServiceProvider lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }

    private IEntIdentityRoleRepository RoleRepository =>
        _lazyServiceProvider.LazyGetRequiredService<IEntIdentityRoleRepository>();

    private IEntIdentityUserRepository UserRepository =>
        _lazyServiceProvider.LazyGetRequiredService<IEntIdentityUserRepository>();

    private ICancellationTokenProvider CancellationTokenProvider =>
        _lazyServiceProvider.LazyGetRequiredService<ICancellationTokenProvider>();

    private CancellationToken CancellationToken => CancellationTokenProvider.Token;

    public virtual async Task<EntIdentityUser> GetByRefreshTokenAsync(string refreshToken)
    {
        var user = await FindByRefreshTokenAsync(refreshToken);
        EntCheck.NotNull(user,nameof(user));
        return user!;
    }
    
    public virtual async Task<EntIdentityUser?> FindByRefreshTokenAsync(string refreshToken)
    {
        EntCheck.NotNullOrWhiteSpace(refreshToken,nameof(refreshToken));
        return await UserRepository.FindAsync(x => x.RefreshToken.Equals(refreshToken),includeDetails:true, cancellationToken: CancellationToken);
    }

    public virtual async Task<EntIdentityUser?> FindByIdAsync(Guid id)
    {
        EntCheck.NotNull(id,nameof(id));
        return await UserRepository.FindAsync(x => x.Id.Equals(id),includeDetails:true, cancellationToken: CancellationToken);
    }
    public virtual async Task<EntIdentityUser?> FindByUserNameAsync(string userName)
    {
        EntCheck.NotNullOrWhiteSpace(userName,nameof(userName));
        return await UserRepository.FindAsync(x => x.UserName.Equals(userName),includeDetails:true, cancellationToken: CancellationToken);
    }
    
    public virtual async Task<EntIdentityUser?> FindByPhoneNumberAsync(string phoneNumber)
    {
        EntCheck.NotNullOrWhiteSpace(phoneNumber,nameof(phoneNumber));
        return await UserRepository.FindAsync(x => x.PhoneNumber != null && x.PhoneNumber.Equals(phoneNumber),includeDetails:true, cancellationToken: CancellationToken);
    }
    
    public virtual async Task<EntIdentityUser?> FindByEmailAsync(string email)
    {
        EntCheck.NotNullOrWhiteSpace(email,nameof(email));
        return await UserRepository.FindAsync(x => x.Email != null && x.Email.Equals(email),includeDetails:true, cancellationToken: CancellationToken);
    }
    
    public virtual async Task CreateAsync(EntIdentityUser user, string password,bool autoSave = true)
    {
        user.SetPassword(password);
        await UserRepository.InsertAsync(user, autoSave, CancellationToken);
    }

    public virtual async Task UpdateAsync(EntIdentityUser user,bool autoSave = true)
    {
        await UserRepository.UpdateAsync(user, autoSave, CancellationToken);
    }
    
    public virtual async Task DeleteAsync(EntIdentityUser user,bool autoSave = true)
    {
        await UserRepository.DeleteAsync(user, autoSave, CancellationToken);
    }

    public virtual Task UpdatePasswordHashAsync(EntIdentityUser user, string password)
    {
        user.SetPassword(password);
        return Task.CompletedTask;
    }

    
    
    public virtual Task<bool> ComparePasswordHashAsync(EntIdentityUser user, string rawPassword)
    {
         return Task.FromResult(user.ComparePassword(rawPassword));
    }

    public async Task AssignRoleAsync(EntIdentityUser user,string name,bool autoSave = true , bool isForce = false)
    {
        var role = await RoleRepository.FindAsync(x => x.Name.Equals(name), cancellationToken: CancellationToken);
        
        if (isForce && role == null)
            role = await RoleRepository.InsertAsync(new EntIdentityRole(Guid.NewGuid(), name), cancellationToken: CancellationToken);
        
        EntCheck.NotNull(role, nameof(role));    
        
        user.AssignRole(role!);
        
        if (autoSave)
            await UpdateAsync(user);
    }
    
    public async Task UnAssignRole(EntIdentityUser user,string name,bool autoSave = true)
    {
        var role = await RoleRepository.GetAsync(x => x.Name.Equals(name), cancellationToken: CancellationToken);
        EntCheck.NotNull(role, nameof(role));
        
        user.UnAssignRole(role);

        if (autoSave)
            await UpdateAsync(user);
    }
    
    public Task<List<EntIdentityRole>> Roles(EntIdentityUser user)
    {
        return Task.FromResult(user.Roles.ToList());
    }
    
    public async Task<bool> CheckRole(EntIdentityUser user,string name)
    {
        var role = await RoleRepository.FindAsync(x => x.Name.Equals(name), cancellationToken: CancellationToken);
        return role != null && user.CheckRole(role!);
    }
}