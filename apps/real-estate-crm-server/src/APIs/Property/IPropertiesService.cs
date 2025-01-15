using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IPropertiesService
{
    /// <summary>
    /// Create one Property
    /// </summary>
    public Task<Property> CreateProperty(PropertyCreateInput property);

    /// <summary>
    /// Delete one Property
    /// </summary>
    public Task DeleteProperty(PropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Properties
    /// </summary>
    public Task<List<Property>> Properties(PropertyFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Property records
    /// </summary>
    public Task<MetadataDto> PropertiesMeta(PropertyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Property
    /// </summary>
    public Task<Property> Property(PropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Property
    /// </summary>
    public Task UpdateProperty(PropertyWhereUniqueInput uniqueId, PropertyUpdateInput updateDto);
}
