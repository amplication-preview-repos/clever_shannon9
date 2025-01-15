using Microsoft.AspNetCore.Mvc;

namespace RealEstateCrm.APIs;

[ApiController()]
public class AgentsController : AgentsControllerBase
{
    public AgentsController(IAgentsService service)
        : base(service) { }
}
