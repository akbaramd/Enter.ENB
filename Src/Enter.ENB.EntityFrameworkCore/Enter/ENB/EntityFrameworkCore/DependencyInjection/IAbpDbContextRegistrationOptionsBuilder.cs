using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;


public interface IAbpDbContextRegistrationOptionsBuilder : IAbpCommonDbContextRegistrationOptionsBuilder
{
    void Entity<TEntity>(Action<AbpEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntEntity;
}
