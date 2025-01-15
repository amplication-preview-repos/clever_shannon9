using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs.Extensions;

public static class ConnectionStringsExtensions
{
    public static ConnectionString ToDto(this ConnectionStringDbModel model)
    {
        return new ConnectionString
        {
            ConnectionString = model.ConnectionString,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ConnectionStringDbModel ToModel(
        this ConnectionStringUpdateInput updateDto,
        ConnectionStringWhereUniqueInput uniqueId
    )
    {
        var connectionString = new ConnectionStringDbModel
        {
            Id = uniqueId.Id,
            ConnectionString = updateDto.ConnectionString
        };

        if (updateDto.CreatedAt != null)
        {
            connectionString.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            connectionString.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            connectionString.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return connectionString;
    }
}
