using Microsoft.EntityFrameworkCore;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;
using MultiTenantApi.APIs.Extensions;
using MultiTenantApi.Infrastructure;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs;

public abstract class ConnectionStringsServiceBase : IConnectionStringsService
{
    protected readonly MultiTenantApiDbContext _context;

    public ConnectionStringsServiceBase(MultiTenantApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one ConnectionString
    /// </summary>
    public async Task<ConnectionString> CreateConnectionString(
        ConnectionStringCreateInput createDto
    )
    {
        var connectionString = new ConnectionStringDbModel
        {
            ConnectionString = createDto.ConnectionString,
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            connectionString.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            connectionString.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.ConnectionStrings.Add(connectionString);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ConnectionStringDbModel>(connectionString.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one ConnectionString
    /// </summary>
    public async Task DeleteConnectionString(ConnectionStringWhereUniqueInput uniqueId)
    {
        var connectionString = await _context.ConnectionStrings.FindAsync(uniqueId.Id);
        if (connectionString == null)
        {
            throw new NotFoundException();
        }

        _context.ConnectionStrings.Remove(connectionString);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many ConnectionStrings
    /// </summary>
    public async Task<List<ConnectionString>> ConnectionStrings(
        ConnectionStringFindManyArgs findManyArgs
    )
    {
        var connectionStrings = await _context
            .ConnectionStrings.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return connectionStrings.ConvertAll(connectionString => connectionString.ToDto());
    }

    /// <summary>
    /// Meta data about ConnectionString records
    /// </summary>
    public async Task<MetadataDto> ConnectionStringsMeta(ConnectionStringFindManyArgs findManyArgs)
    {
        var count = await _context.ConnectionStrings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one ConnectionString
    /// </summary>
    public async Task<ConnectionString> ConnectionString(ConnectionStringWhereUniqueInput uniqueId)
    {
        var connectionStrings = await this.ConnectionStrings(
            new ConnectionStringFindManyArgs
            {
                Where = new ConnectionStringWhereInput { Id = uniqueId.Id }
            }
        );
        var connectionString = connectionStrings.FirstOrDefault();
        if (connectionString == null)
        {
            throw new NotFoundException();
        }

        return connectionString;
    }

    /// <summary>
    /// Update one ConnectionString
    /// </summary>
    public async Task UpdateConnectionString(
        ConnectionStringWhereUniqueInput uniqueId,
        ConnectionStringUpdateInput updateDto
    )
    {
        var connectionString = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            connectionString.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(connectionString).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ConnectionStrings.Any(e => e.Id == connectionString.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a customer record for ConnectionString
    /// </summary>
    public async Task<Customer> GetCustomer(ConnectionStringWhereUniqueInput uniqueId)
    {
        var connectionString = await _context
            .ConnectionStrings.Where(connectionString => connectionString.Id == uniqueId.Id)
            .Include(connectionString => connectionString.Customer)
            .FirstOrDefaultAsync();
        if (connectionString == null)
        {
            throw new NotFoundException();
        }
        return connectionString.Customer.ToDto();
    }
}
