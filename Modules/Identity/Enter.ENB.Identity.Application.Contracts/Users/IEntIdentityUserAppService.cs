using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Users;

public interface IEntIdentityUserAppService : ICrudAppService<EntIdentityUserDto,Guid,PagedAndSortedResultRequestDto,EntIdentityUserCreateDto,EntIdentityUserUpdateDto> 
{
    Task<EntIdentityUserDto> GetByUsernameAsync(string userName);
    Task AssignRoleAsync(Guid userId, EntIdentityUserAssignRoleDto input);
    Task UnAssignRoleAsync(Guid userId,EntIdentityUserUnAssignRoleDto input);
    Task<bool> ExistsRoleAsync(Guid userId,EntIdentityUserExistsRoleDto input);
}

