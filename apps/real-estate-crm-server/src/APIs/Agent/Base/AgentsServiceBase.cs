using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class AgentsServiceBase : IAgentsService
{
    protected readonly RealEstateCrmDbContext _context;

    public AgentsServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Agent
    /// </summary>
    public async Task<Agent> CreateAgent(AgentCreateInput createDto)
    {
        var agent = new AgentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            agent.Id = createDto.Id;
        }

        _context.Agents.Add(agent);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AgentDbModel>(agent.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Agent
    /// </summary>
    public async Task DeleteAgent(AgentWhereUniqueInput uniqueId)
    {
        var agent = await _context.Agents.FindAsync(uniqueId.Id);
        if (agent == null)
        {
            throw new NotFoundException();
        }

        _context.Agents.Remove(agent);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Agents
    /// </summary>
    public async Task<List<Agent>> Agents(AgentFindManyArgs findManyArgs)
    {
        var agents = await _context
            .Agents.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return agents.ConvertAll(agent => agent.ToDto());
    }

    /// <summary>
    /// Meta data about Agent records
    /// </summary>
    public async Task<MetadataDto> AgentsMeta(AgentFindManyArgs findManyArgs)
    {
        var count = await _context.Agents.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Agent
    /// </summary>
    public async Task<Agent> Agent(AgentWhereUniqueInput uniqueId)
    {
        var agents = await this.Agents(
            new AgentFindManyArgs { Where = new AgentWhereInput { Id = uniqueId.Id } }
        );
        var agent = agents.FirstOrDefault();
        if (agent == null)
        {
            throw new NotFoundException();
        }

        return agent;
    }

    /// <summary>
    /// Update one Agent
    /// </summary>
    public async Task UpdateAgent(AgentWhereUniqueInput uniqueId, AgentUpdateInput updateDto)
    {
        var agent = updateDto.ToModel(uniqueId);

        _context.Entry(agent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Agents.Any(e => e.Id == agent.Id))
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
