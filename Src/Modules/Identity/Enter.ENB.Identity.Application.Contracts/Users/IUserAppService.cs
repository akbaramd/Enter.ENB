using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Users;

public interface IUserAppService : ICrudAppService<UserDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateUserDto> 
{
    
}