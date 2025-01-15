using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;

namespace RealEstateCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AgentsControllerBase : ControllerBase
{
    protected readonly IAgentsService _service;

    public AgentsControllerBase(IAgentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Agent
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Agent>> CreateAgent(AgentCreateInput input)
    {
        var agent = await _service.CreateAgent(input);

        return CreatedAtAction(nameof(Agent), new { id = agent.Id }, agent);
    }

    /// <summary>
    /// Delete one Agent
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAgent([FromRoute()] AgentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAgent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Agents
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Agent>>> Agents([FromQuery()] AgentFindManyArgs filter)
    {
        return Ok(await _service.Agents(filter));
    }

    /// <summary>
    /// Meta data about Agent records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AgentsMeta([FromQuery()] AgentFindManyArgs filter)
    {
        return Ok(await _service.AgentsMeta(filter));
    }

    /// <summary>
    /// Get one Agent
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Agent>> Agent([FromRoute()] AgentWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Agent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Agent
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAgent(
        [FromRoute()] AgentWhereUniqueInput uniqueId,
        [FromQuery()] AgentUpdateInput agentUpdateDto
    )
    {
        try
        {
            await _service.UpdateAgent(uniqueId, agentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
