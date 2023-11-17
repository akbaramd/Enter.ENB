using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;


public interface IEntDbContextRegistrationOptionsBuilder : IEntCommonDbContextRegistrationOptionsBuilder
{
    void Entity<TEntity>(Action<EntEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntEntity;
}
