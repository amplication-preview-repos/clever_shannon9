using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs.Extensions;

public static class UserTokensExtensions
{
    public static UserToken ToDto(this UserTokenDbModel model)
    {
        return new UserToken
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Expiry = model.Expiry,
            Id = model.Id,
            Token = model.Token,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static UserTokenDbModel ToModel(
        this UserTokenUpdateInput updateDto,
        UserTokenWhereUniqueInput uniqueId
    )
    {
        var userToken = new UserTokenDbModel
        {
            Id = uniqueId.Id,
            Expiry = updateDto.Expiry,
            Token = updateDto.Token
        };

        if (updateDto.CreatedAt != null)
        {
            userToken.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            userToken.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            userToken.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return userToken;
    }
}
