using AutoMapper;
using Enter.ENB.AutoMapper.Enter.ENB.AutoMapper;
using Enter.ENB.Exceptions;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;

namespace Enter.ENB.AutoMapper.Enter.ENB.ObjectMapping;

public static class EntAutoMapperObjectMapperExtensions
{
    public static IMapper GetMapper(this IObjectMapper objectMapper)
    {
        return objectMapper.AutoObjectMappingProvider.GetMapper();
    }

    public static IMapper GetMapper(this IAutoObjectMappingProvider autoObjectMappingProvider)
    {
        if (autoObjectMappingProvider is AutoMapperAutoObjectMappingProvider autoMapperAutoObjectMappingProvider)
        {
            return autoMapperAutoObjectMappingProvider.MapperAccessor.Mapper;
        }

        throw new EntException($"Given object is not an instance of {typeof(AutoMapperAutoObjectMappingProvider).AssemblyQualifiedName}. The type of the given object it {autoObjectMappingProvider.GetType().AssemblyQualifiedName}");
    }
}
