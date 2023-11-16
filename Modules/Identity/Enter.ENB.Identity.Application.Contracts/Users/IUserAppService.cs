using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Users;

public interface IUserAppService : ICrudAppService<EntUserDto,Guid,PagedAndSortedResultRequestDto,UserCreateDto,UserUpdateDto> 
{
    Task<EntUserDto> GetByUsernameAsync(string userName);
}