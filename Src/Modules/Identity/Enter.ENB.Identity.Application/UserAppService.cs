using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.Application;

public class UserAppService :ApplicationService, IUserAppService
{
    private readonly IEntUserRepository _repository;
    
    

    public async Task<UserDto> GetAsync(Guid id)
    {
        var res = await _repository.Find(id);
        return ObjectMapper.Map<EntUser,UserDto>(res);
    }

    public Task<PagedResultDto<UserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> CreateAsync(UserDto input)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(Guid id, UserDto input)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
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

    public UserAppService(IEntLazyServiceProvider lazyServiceProvider, IEntUserRepository repository) : base(lazyServiceProvider)
    {
        _repository = repository;
    }
}