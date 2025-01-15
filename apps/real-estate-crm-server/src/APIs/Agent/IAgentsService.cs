using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IAgentsService
{
    /// <summary>
    /// Create one Agent
    /// </summary>
    public Task<Agent> CreateAgent(AgentCreateInput agent);

    /// <summary>
    /// Delete one Agent
    /// </summary>
    public Task DeleteAgent(AgentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Agents
    /// </summary>
    public Task<List<Agent>> Agents(AgentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Agent records
    /// </summary>
    public Task<MetadataDto> AgentsMeta(AgentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Agent
    /// </summary>
    public Task<Agent> Agent(AgentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Agent
    /// </summary>
    public Task UpdateAgent(AgentWhereUniqueInput uniqueId, AgentUpdateInput updateDto);
}
