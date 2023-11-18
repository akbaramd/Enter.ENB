using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Managers;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[ExposeServices(
    typeof(IEntIdentityUserAppService)
    )]
public class EntIdentityUserAppService :EntIdentityAppServiceBase, IEntIdentityUserAppService
{
    public IEntIdentityUserRepository Repository => LazyServiceProvider.GetRequiredService<IEntIdentityUserRepository>();
    public IdentityUserManager UserManager => LazyServiceProvider.GetRequiredService<IdentityUserManager>();
    
    public async Task<EntIdentityUserDto> GetAsync(Guid id)
    {
        var user = await UserManager.FindByIdAsync(id);
        return ObjectMapper.Map<EntIdentityUser,EntIdentityUserDto>(user);
    }

    public async Task<PagedResultDto<EntIdentityUserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var list = await Repository.GetPagedListAsync(input.SkipCount,input.MaxResultCount,input.Sorting);
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<EntIdentityUserDto>(
            totalCount,
            ObjectMapper.Map<List<EntIdentityUser>, List<EntIdentityUserDto>>(list)
        );
    }

    public async Task<EntIdentityUserDto> CreateAsync(EntIdentityUserCreateDto input)
    {
        var user = new EntIdentityUser(Guid.NewGuid(),input.UserName);
        user.SetName(input.FirstName,input.LastName);
        user.SetPhoneNumber(input.PhoneNumber);

        await UserManager.CreateAsync(user, input.Password, true);
        return ObjectMapper.Map<EntIdentityUser,EntIdentityUserDto>(user);
    }

    public async Task<EntIdentityUserDto> UpdateAsync(Guid id, EntIdentityUserUpdateDto input)
    {
        var user = await Repository.GetAsync(id);
        user.SetName(input.FirstName,input.LastName);
        user.SetPhoneNumber(input.PhoneNumber);
        await UserManager.UpdateAsync(user);
        return ObjectMapper.Map<EntIdentityUser,EntIdentityUserDto>(user);
    }
    
    public async Task<EntIdentityUserDto> AssignRoleAsync(Guid id, EntIdentityUserUpdateDto input)
    {
        var user = await Repository.GetAsync(id);
        user.SetName(input.FirstName,input.LastName);
        user.SetPhoneNumber(input.PhoneNumber);
        await UserManager.UpdateAsync(user);
        return ObjectMapper.Map<EntIdentityUser,EntIdentityUserDto>(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await Repository.GetAsync(id);
        await UserManager.DeleteAsync(user);
    }

    public async Task<EntIdentityUserDto> GetByUsernameAsync(string userName)
    {
        var find = await Repository.FindAsync(x => x.UserName.Equals(userName));

        if (find == null)
        {
            throw new EntityNotFoundException(typeof(EntIdentityUser), userName);
        }

        return ObjectMapper.Map<EntIdentityUser, EntIdentityUserDto>(find);
    }

    public async Task AssignRoleAsync(Guid userId,EntIdentityUserAssignRoleDto input)
    {
        var user = await UserManager.FindByIdAsync(userId);
        EntCheck.NotNull(user, nameof(user));
        await  UserManager.AssignRoleAsync(user, input.RoleName,isForce:input.IsForce);
    }
    public async Task UnAssignRoleAsync(Guid userId,EntIdentityUserUnAssignRoleDto input)
    {
        var user = await UserManager.FindByIdAsync(userId);
        EntCheck.NotNull(user, nameof(user));
        await  UserManager.UnAssignRole(user, input.RoleName);
    }
    public async Task<bool> ExistsRoleAsync(Guid userId,EntIdentityUserExistsRoleDto input)
    {
        var user = await UserManager.FindByIdAsync(userId);
        EntCheck.NotNull(user, nameof(user));
        return await  UserManager.CheckRole(user, input.RoleName);
    }
}