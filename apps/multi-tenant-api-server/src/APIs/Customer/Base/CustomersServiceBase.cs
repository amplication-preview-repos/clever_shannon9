using Microsoft.EntityFrameworkCore;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;
using MultiTenantApi.APIs.Extensions;
using MultiTenantApi.Infrastructure;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly MultiTenantApiDbContext _context;

    public CustomersServiceBase(MultiTenantApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            Phone = createDto.Phone,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.ConnectionStrings != null)
        {
            customer.ConnectionStrings = await _context
                .ConnectionStrings.Where(connectionString =>
                    createDto.ConnectionStrings.Select(t => t.Id).Contains(connectionString.Id)
                )
                .ToListAsync();
        }

        if (createDto.UserTokens != null)
        {
            customer.UserTokens = await _context
                .UserTokens.Where(userToken =>
                    createDto.UserTokens.Select(t => t.Id).Contains(userToken.Id)
                )
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.ConnectionStrings)
            .Include(x => x.UserTokens)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.ConnectionStrings != null)
        {
            customer.ConnectionStrings = await _context
                .ConnectionStrings.Where(connectionString =>
                    updateDto.ConnectionStrings.Select(t => t).Contains(connectionString.Id)
                )
                .ToListAsync();
        }

        if (updateDto.UserTokens != null)
        {
            customer.UserTokens = await _context
                .UserTokens.Where(userToken =>
                    updateDto.UserTokens.Select(t => t).Contains(userToken.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
    /// Connect multiple ConnectionStrings records to Customer
    /// </summary>
    public async Task ConnectConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.ConnectionStrings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ConnectionStrings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.ConnectionStrings);

        foreach (var child in childrenToConnect)
        {
            parent.ConnectionStrings.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple ConnectionStrings records from Customer
    /// </summary>
    public async Task DisconnectConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.ConnectionStrings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ConnectionStrings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.ConnectionStrings?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple ConnectionStrings records for Customer
    /// </summary>
    public async Task<List<ConnectionString>> FindConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringFindManyArgs customerFindManyArgs
    )
    {
        var connectionStrings = await _context
            .ConnectionStrings.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return connectionStrings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple ConnectionStrings records for Customer
    /// </summary>
    public async Task UpdateConnectionStrings(
        CustomerWhereUniqueInput uniqueId,
        ConnectionStringWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.ConnectionStrings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ConnectionStrings.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.ConnectionStrings = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple UserTokens records to Customer
    /// </summary>
    public async Task ConnectUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.UserTokens)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserTokens.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.UserTokens);

        foreach (var child in childrenToConnect)
        {
            parent.UserTokens.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple UserTokens records from Customer
    /// </summary>
    public async Task DisconnectUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.UserTokens)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserTokens.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.UserTokens?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple UserTokens records for Customer
    /// </summary>
    public async Task<List<UserToken>> FindUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenFindManyArgs customerFindManyArgs
    )
    {
        var userTokens = await _context
            .UserTokens.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return userTokens.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple UserTokens records for Customer
    /// </summary>
    public async Task UpdateUserTokens(
        CustomerWhereUniqueInput uniqueId,
        UserTokenWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.UserTokens)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserTokens.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.UserTokens = children;
        await _context.SaveChangesAsync();
    }
}
