using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;


public class UserAppService :IdentityAppServiceBase, IUserAppService
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public UserAppService(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }

    public IEntUserRepository Repository => _lazyServiceProvider.GetRequiredService<IEntUserRepository>();
    
    public async Task<EntUserDto> GetAsync(Guid id)
    {
        var user = await Repository.GetAsync(id);
        return ObjectMapper.Map<EntIdentityUser,EntUserDto>(user);
    }

    public async Task<PagedResultDto<EntUserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var list = await Repository.GetPagedListAsync(input.SkipCount,input.MaxResultCount,input.Sorting);
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<EntUserDto>(
            totalCount,
            ObjectMapper.Map<List<EntIdentityUser>, List<EntUserDto>>(list)
        );
    }

    public async Task<EntUserDto> CreateAsync(UserCreateDto input)
    {
        var user = new EntIdentityUser(Guid.NewGuid(),input.UserName);
        user.SetName(input.FirstName,input.LastName);
        user.SetPassword(input.Password);
        user.SetPhoneNumber(input.PhoneNumber);
        await Repository.InsertAsync(user,true);
        return ObjectMapper.Map<EntIdentityUser,EntUserDto>(user);
    }

    public async Task<EntUserDto> UpdateAsync(Guid id, UserUpdateDto input)
    {
        var user = await Repository.GetAsync(id);
        user.SetName(input.FirstName,input.LastName);
        user.SetPhoneNumber(input.PhoneNumber);
        await Repository.UpdateAsync(user,true);
        return ObjectMapper.Map<EntIdentityUser,EntUserDto>(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await Repository.GetAsync(id);
        await Repository.DeleteAsync(user,true);
    }

    public async Task<EntUserDto> GetByUsernameAsync(string userName)
    {
        var find = await Repository.FindAsync(x => x.UserName.Equals(userName));

        if (find == null)
        {
            throw new EntityNotFoundException(typeof(EntIdentityUser), userName);
        }

        return ObjectMapper.Map<EntIdentityUser, EntUserDto>(find);
    }
}