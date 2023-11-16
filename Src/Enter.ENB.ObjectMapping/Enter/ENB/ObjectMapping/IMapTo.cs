namespace Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;

public interface IMapTo<TDestination>
{
    TDestination MapTo();

    void MapTo(TDestination destination);
}
