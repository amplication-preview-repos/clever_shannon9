using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class ClientsExtensions
{
    public static Client ToDto(this ClientDbModel model)
    {
        return new Client
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClientDbModel ToModel(
        this ClientUpdateInput updateDto,
        ClientWhereUniqueInput uniqueId
    )
    {
        var client = new ClientDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            client.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            client.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return client;
    }
}
