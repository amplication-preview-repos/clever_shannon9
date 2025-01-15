using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class PropertiesExtensions
{
    public static Property ToDto(this PropertyDbModel model)
    {
        return new Property
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PropertyDbModel ToModel(
        this PropertyUpdateInput updateDto,
        PropertyWhereUniqueInput uniqueId
    )
    {
        var property = new PropertyDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            property.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            property.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return property;
    }
}
