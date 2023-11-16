using Enter.ENB.Auditing;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Extensions;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey>
    : ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected ReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository,IEntLazyServiceProvider lazyServiceProvider)
        : base(repository,lazyServiceProvider)
    {

    }
}

public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey, TGetListInput>
    : ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected ReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository,IEntLazyServiceProvider lazyServiceProvider)
        : base(repository,lazyServiceProvider)
    {

    }
}

public abstract class ReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
    : AbstractKeyReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
    where TEntity : class, IEntEntity<TKey>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
{
    protected IReadOnlyRepository<TEntity, TKey> Repository { get; }

    protected ReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository,IEntLazyServiceProvider lazyServiceProvider)
    : base(repository,lazyServiceProvider)
    {
        Repository = repository;
    }

    protected override async Task<TEntity> GetEntityByIdAsync(TKey id)
    {
        return await Repository.GetAsync(id);
    }

    protected override IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
    {
        if (typeof(TEntity).IsAssignableTo<ICreationAuditedObject>())
        {
            return query.OrderByDescending(e => ((ICreationAuditedObject)e).CreationTime);
        }
        else
        {
            return query.OrderByDescending(e => e.Id);
        }
    }
}
