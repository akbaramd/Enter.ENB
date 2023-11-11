using Enter.ENB.Domain.Repository;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Domain.Users;

namespace Enter.ENB.Identity.Application.Users;

public class UserAppService : IdentityAppService<EntUser,Guid>, IUserAppService
{
    public UserAppService(IRepository<EntUser, Guid> repository) : base(repository)
    {
    }
}