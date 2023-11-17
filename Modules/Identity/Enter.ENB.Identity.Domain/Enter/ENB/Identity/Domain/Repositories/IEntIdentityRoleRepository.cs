using Enter.ENB.Domain.Repository;

namespace Enter.ENB.Identity.Domain.Repositories;

public interface IEntIdentityUserRepository : IRepository<EntIdentityUser,Guid>
{
    Task<EntIdentityUser?> GetByPhoneNumberAsync(string phoneNumber);
}