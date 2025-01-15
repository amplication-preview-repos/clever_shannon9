using Microsoft.EntityFrameworkCore;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;
using MultiTenantApi.APIs.Extensions;
using MultiTenantApi.Infrastructure;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs;

public abstract class UserTokensServiceBase : IUserTokensService
{
    protected readonly MultiTenantApiDbContext _context;

    public UserTokensServiceBase(MultiTenantApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one UserToken
    /// </summary>
    public async Task<UserToken> CreateUserToken(UserTokenCreateInput createDto)
    {
        var userToken = new UserTokenDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Expiry = createDto.Expiry,
            Token = createDto.Token,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            userToken.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            userToken.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.UserTokens.Add(userToken);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserTokenDbModel>(userToken.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one UserToken
    /// </summary>
    public async Task DeleteUserToken(UserTokenWhereUniqueInput uniqueId)
    {
        var userToken = await _context.UserTokens.FindAsync(uniqueId.Id);
        if (userToken == null)
        {
            throw new NotFoundException();
        }

        _context.UserTokens.Remove(userToken);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many UserTokens
    /// </summary>
    public async Task<List<UserToken>> UserTokens(UserTokenFindManyArgs findManyArgs)
    {
        var userTokens = await _context
            .UserTokens.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return userTokens.ConvertAll(userToken => userToken.ToDto());
    }

    /// <summary>
    /// Meta data about UserToken records
    /// </summary>
    public async Task<MetadataDto> UserTokensMeta(UserTokenFindManyArgs findManyArgs)
    {
        var count = await _context.UserTokens.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one UserToken
    /// </summary>
    public async Task<UserToken> UserToken(UserTokenWhereUniqueInput uniqueId)
    {
        var userTokens = await this.UserTokens(
            new UserTokenFindManyArgs { Where = new UserTokenWhereInput { Id = uniqueId.Id } }
        );
        var userToken = userTokens.FirstOrDefault();
        if (userToken == null)
        {
            throw new NotFoundException();
        }

        return userToken;
    }

    /// <summary>
    /// Update one UserToken
    /// </summary>
    public async Task UpdateUserToken(
        UserTokenWhereUniqueInput uniqueId,
        UserTokenUpdateInput updateDto
    )
    {
        var userToken = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            userToken.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(userToken).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.UserTokens.Any(e => e.Id == userToken.Id))
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
    /// Get a customer record for UserToken
    /// </summary>
    public async Task<Customer> GetCustomer(UserTokenWhereUniqueInput uniqueId)
    {
        var userToken = await _context
            .UserTokens.Where(userToken => userToken.Id == uniqueId.Id)
            .Include(userToken => userToken.Customer)
            .FirstOrDefaultAsync();
        if (userToken == null)
        {
            throw new NotFoundException();
        }
        return userToken.Customer.ToDto();
    }
}
