using RealEstateCrm.Infrastructure;

namespace RealEstateCrm.APIs;

public class AgentsService : AgentsServiceBase
{
    public AgentsService(RealEstateCrmDbContext context)
        : base(context) { }
}
