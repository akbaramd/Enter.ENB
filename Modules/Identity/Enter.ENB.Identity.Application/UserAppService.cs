using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.Application;


public class UserAppService :IdentityAppServiceBase, IUserAppService
{
    private readonly IEntLazyServiceProvider _lazyServiceProvider;

    public UserAppService(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }

    public IEntUserRepository Repository => _lazyServiceProvider.LazyGetRequiredService<IEntUserRepository>();
    
    public Task<UserDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<UserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> CreateAsync(CreateUpdateUserDto input)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto input)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetByUsernameAsync(string userName)
    {
        var find = await Repository.FindAsync(x => x.UserName.Equals(userName));

        if (find == null)
        {
            throw new EntityNotFoundException(typeof(EntUser), userName);
        }

        return ObjectMapper.Map<EntUser, UserDto>(find);
    }
}