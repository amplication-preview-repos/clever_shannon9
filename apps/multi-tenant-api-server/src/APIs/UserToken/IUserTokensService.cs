using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;

namespace MultiTenantApi.APIs;

public interface IUserTokensService
{
    /// <summary>
    /// Create one UserToken
    /// </summary>
    public Task<UserToken> CreateUserToken(UserTokenCreateInput usertoken);

    /// <summary>
    /// Delete one UserToken
    /// </summary>
    public Task DeleteUserToken(UserTokenWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many UserTokens
    /// </summary>
    public Task<List<UserToken>> UserTokens(UserTokenFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about UserToken records
    /// </summary>
    public Task<MetadataDto> UserTokensMeta(UserTokenFindManyArgs findManyArgs);

    /// <summary>
    /// Get one UserToken
    /// </summary>
    public Task<UserToken> UserToken(UserTokenWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one UserToken
    /// </summary>
    public Task UpdateUserToken(UserTokenWhereUniqueInput uniqueId, UserTokenUpdateInput updateDto);

    /// <summary>
    /// Get a customer record for UserToken
    /// </summary>
    public Task<Customer> GetCustomer(UserTokenWhereUniqueInput uniqueId);
}
