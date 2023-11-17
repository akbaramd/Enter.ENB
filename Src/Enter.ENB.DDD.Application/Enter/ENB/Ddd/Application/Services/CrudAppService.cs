using Enter.ENB.Auditing;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Extensions;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class CrudAppService<TEntity, TEntityDto, TKey>
    : CrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected CrudAppService(IRepository<TEntity, TKey> repository)
        : base(repository)
    {

    }
}

public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
    : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected CrudAppService(IRepository<TEntity, TKey> repository)
        : base(repository)
    {

    }
}

public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
    : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected CrudAppService(IRepository<TEntity, TKey> repository)
        : base(repository)
    {

    }
}

public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : CrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
{
    protected CrudAppService(IRepository<TEntity, TKey> repository)
        : base(repository)
    {

    }

    protected override Task<TEntityDto> MapToGetListOutputDtoAsync(TEntity entity)
    {
        return MapToGetOutputDtoAsync(entity);
    }

    protected override TEntityDto MapToGetListOutputDto(TEntity entity)
    {
        return MapToGetOutputDto(entity);
    }
}

public abstract class CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : AbstractKeyCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntEntity<TKey>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
{
    protected new IRepository<TEntity, TKey> Repository { get; }

    protected CrudAppService(IRepository<TEntity, TKey> repository)
        : base(repository)
    {
        Repository = repository;
    }

    protected override async Task DeleteByIdAsync(TKey id)
    {
        await Repository.DeleteAsync(id);
    }

    protected override async Task<TEntity> GetEntityByIdAsync(TKey id)
    {
        return await Repository.GetAsync(id);
    }

    protected override void MapToEntity(TUpdateInput updateInput, TEntity entity)
    {
        if (updateInput is IEntityDto<TKey> entityDto)
        {
            entityDto.Id = entity.Id;
        }

        base.MapToEntity(updateInput, entity);
    }

    protected override IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
    {
        if (typeof(TEntity).IsAssignableTo<IHasCreationTime>())
        {
            return query.OrderByDescending(e => ((IHasCreationTime)e).CreationTime);
        }
        else
        {
            return query.OrderByDescending(e => e.Id);
        }
    }
}
