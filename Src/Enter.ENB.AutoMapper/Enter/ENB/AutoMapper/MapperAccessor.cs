using AutoMapper;

namespace Enter.ENB.AutoMapper;

internal class MapperAccessor : IMapperAccessor
{
    public IMapper Mapper { get; set; } = default!;
}
