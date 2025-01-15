using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class AgentsExtensions
{
    public static Agent ToDto(this AgentDbModel model)
    {
        return new Agent
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AgentDbModel ToModel(
        this AgentUpdateInput updateDto,
        AgentWhereUniqueInput uniqueId
    )
    {
        var agent = new AgentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            agent.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            agent.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return agent;
    }
}
