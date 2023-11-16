namespace Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;

public interface IMapFrom<in TSource>
{
    void MapFrom(TSource source);
}
