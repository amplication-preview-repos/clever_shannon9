using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class PropertiesServiceBase : IPropertiesService
{
    protected readonly RealEstateCrmDbContext _context;

    public PropertiesServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Property
    /// </summary>
    public async Task<Property> CreateProperty(PropertyCreateInput createDto)
    {
        var property = new PropertyDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            property.Id = createDto.Id;
        }

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PropertyDbModel>(property.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Property
    /// </summary>
    public async Task DeleteProperty(PropertyWhereUniqueInput uniqueId)
    {
        var property = await _context.Properties.FindAsync(uniqueId.Id);
        if (property == null)
        {
            throw new NotFoundException();
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Properties
    /// </summary>
    public async Task<List<Property>> Properties(PropertyFindManyArgs findManyArgs)
    {
        var properties = await _context
            .Properties.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return properties.ConvertAll(property => property.ToDto());
    }

    /// <summary>
    /// Meta data about Property records
    /// </summary>
    public async Task<MetadataDto> PropertiesMeta(PropertyFindManyArgs findManyArgs)
    {
        var count = await _context.Properties.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Property
    /// </summary>
    public async Task<Property> Property(PropertyWhereUniqueInput uniqueId)
    {
        var properties = await this.Properties(
            new PropertyFindManyArgs { Where = new PropertyWhereInput { Id = uniqueId.Id } }
        );
        var property = properties.FirstOrDefault();
        if (property == null)
        {
            throw new NotFoundException();
        }

        return property;
    }

    /// <summary>
    /// Update one Property
    /// </summary>
    public async Task UpdateProperty(
        PropertyWhereUniqueInput uniqueId,
        PropertyUpdateInput updateDto
    )
    {
        var property = updateDto.ToModel(uniqueId);

        _context.Entry(property).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Properties.Any(e => e.Id == property.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
