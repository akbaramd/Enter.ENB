using Enter.ENB.DDD.Application.Services;
using Enter.ENB.Identity.Domain.Users;

namespace Enter.ENB.Identity.Application.Contracts.Users;

public interface IUserAppService : ICrudAppService<EntUser,Guid> , IIdentityAppService
{
    
}