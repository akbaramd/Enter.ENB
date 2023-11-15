using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Exceptions;
using Enter.ENB.MultiTenancy;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.DependencyInjection;

public abstract class AbpCommonDbContextRegistrationOptions : IAbpCommonDbContextRegistrationOptionsBuilder
{
    public Type OriginalDbContextType { get; }

    public IServiceCollection Services { get; }

    public Dictionary<MultiTenantDbContextType, Type?> ReplacedDbContextTypes { get; }

    public Type DefaultRepositoryDbContextType { get; protected set; }

    public Type? DefaultRepositoryImplementationType { get; private set; }

    public Type? DefaultRepositoryImplementationTypeWithoutKey { get; private set; }

    public bool RegisterDefaultRepositories { get; private set; }

    public bool IncludeAllEntitiesForDefaultRepositories { get; private set; }

    public Dictionary<Type, Type> CustomRepositories { get; }

    public List<Type> SpecifiedDefaultRepositories { get; }

    public bool SpecifiedDefaultRepositoryTypes => DefaultRepositoryImplementationType != null && DefaultRepositoryImplementationTypeWithoutKey != null;

    protected AbpCommonDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
    {
        OriginalDbContextType = originalDbContextType;
        Services = services;
        DefaultRepositoryDbContextType = originalDbContextType;
        CustomRepositories = new Dictionary<Type, Type>();
        ReplacedDbContextTypes = new Dictionary<MultiTenantDbContextType, Type?>();
        SpecifiedDefaultRepositories = new List<Type>();
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        return ReplaceDbContext(typeof(TOtherDbContext), multiTenancySides: multiTenancySides);
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext, TTargetDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        return ReplaceDbContext(typeof(TOtherDbContext), typeof(TTargetDbContext), multiTenancySides);
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder ReplaceDbContext(Type otherDbContextType, Type? targetDbContextType = null, MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        if (!otherDbContextType.IsAssignableFrom(OriginalDbContextType))
        {
            throw new EntException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {otherDbContextType.AssemblyQualifiedName}!");
        }

        ReplacedDbContextTypes[new MultiTenantDbContextType(otherDbContextType, multiTenancySides)] = targetDbContextType;

        return this;
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false)
    {
        RegisterDefaultRepositories = true;
        IncludeAllEntitiesForDefaultRepositories = includeAllEntities;

        return this;
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false)
    {
        if (!defaultRepositoryDbContextType.IsAssignableFrom(OriginalDbContextType))
        {
            throw new EntException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {defaultRepositoryDbContextType.AssemblyQualifiedName}!");
        }

        DefaultRepositoryDbContextType = defaultRepositoryDbContextType;

        return AddDefaultRepositories(includeAllEntities);
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false)
    {
        return AddDefaultRepositories(typeof(TDefaultRepositoryDbContext), includeAllEntities);
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddDefaultRepository<TEntity>()
    {
        return AddDefaultRepository(typeof(TEntity));
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddDefaultRepository(Type entityType)
    {
        // EntityHelper.CheckEntity(entityType);

        SpecifiedDefaultRepositories.AddIfNotContains(entityType);

        return this;
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>()
    {
        AddCustomRepository(typeof(TEntity), typeof(TRepository));

        return this;
    }

    public IAbpCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses(
        Type repositoryImplementationType,
        Type repositoryImplementationTypeWithoutKey
        )
    {
        EntCheck.NotNull(repositoryImplementationType, nameof(repositoryImplementationType));
        EntCheck.NotNull(repositoryImplementationTypeWithoutKey, nameof(repositoryImplementationTypeWithoutKey));

        DefaultRepositoryImplementationType = repositoryImplementationType;
        DefaultRepositoryImplementationTypeWithoutKey = repositoryImplementationTypeWithoutKey;

        return this;
    }

    private void AddCustomRepository(Type entityType, Type repositoryType)
    {
        if (!typeof(IEntEntity).IsAssignableFrom(entityType))
        {
            throw new EntException($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntEntity<>).AssemblyQualifiedName}.");
        }

        // if (!typeof(IRepository<,>).IsAssignableFrom(repositoryType))
        // {
        //     throw new EntException($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IBasicRepository<>).AssemblyQualifiedName}.");
        // }

        CustomRepositories[entityType] = repositoryType;
    }
}
