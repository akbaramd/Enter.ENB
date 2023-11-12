namespace Enter.ENB.DependencyInjection;

public interface IExposedServiceTypesProvider
{
    Type[] GetExposedServiceTypes(Type targetType);
}