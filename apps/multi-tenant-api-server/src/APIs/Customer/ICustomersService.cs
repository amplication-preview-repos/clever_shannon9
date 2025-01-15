using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;

namespace MultiTenantApi.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple ConnectionStrings records to Customer
    /// </summary>
    public Task ConnectConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] connectionStringsId
    );

    /// <summary>
    /// Disconnect multiple ConnectionStrings records from Customer
    /// </summary>
    public Task DisconnectConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] connectionStringsId
    );

    /// <summary>
    /// Find multiple ConnectionStrings records for Customer
    /// </summary>
    public Task<List<ConnectionString>> FindConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringFindManyArgs ConnectionStringFindManyArgs
    );

    /// <summary>
    /// Update multiple ConnectionStrings records for Customer
    /// </summary>
    public Task UpdateConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] connectionStringsId
    );

    /// <summary>
    /// Connect multiple UserTokens records to Customer
    /// </summary>
    public Task ConnectUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] userTokensId
    );

    /// <summary>
    /// Disconnect multiple UserTokens records from Customer
    /// </summary>
    public Task DisconnectUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] userTokensId
    );

    /// <summary>
    /// Find multiple UserTokens records for Customer
    /// </summary>
    public Task<List<UserToken>> FindUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenFindManyArgs UserTokenFindManyArgs
    );

    /// <summary>
    /// Update multiple UserTokens records for Customer
    /// </summary>
    public Task UpdateUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] userTokensId
    );
}
