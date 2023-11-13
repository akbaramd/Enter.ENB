using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.Application.Contracts.Users;

public interface IUserAppService : ICrudAppService<EntUser,Guid> 
{
    
}