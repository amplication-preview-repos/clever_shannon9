using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;

namespace MultiTenantApi.APIs;

public interface IConnectionStringsService
{
    /// <summary>
    /// Create one ConnectionString
    /// </summary>
    public Task<ConnectionString> CreateConnectionString(
        ConnectionStringCreateInput connectionstring
    );

    /// <summary>
    /// Delete one ConnectionString
    /// </summary>
    public Task DeleteConnectionString(ConnectionStringWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many ConnectionStrings
    /// </summary>
    public Task<List<ConnectionString>> ConnectionStrings(
        ConnectionStringFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about ConnectionString records
    /// </summary>
    public Task<MetadataDto> ConnectionStringsMeta(ConnectionStringFindManyArgs findManyArgs);

    /// <summary>
    /// Get one ConnectionString
    /// </summary>
    public Task<ConnectionString> ConnectionString(ConnectionStringWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one ConnectionString
    /// </summary>
    public Task UpdateConnectionString(
        ConnectionStringWhereUniqueInput uniqueId,
        ConnectionStringUpdateInput updateDto
    );

    /// <summary>
    /// Get a customer record for ConnectionString
    /// </summary>
    public Task<Customer> GetCustomer(ConnectionStringWhereUniqueInput uniqueId);
}
