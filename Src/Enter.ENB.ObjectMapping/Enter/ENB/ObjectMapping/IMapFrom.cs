namespace Enter.ENB.ObjectMapping;

public interface IMapFrom<in TSource>
{
    void MapFrom(TSource source);
}
