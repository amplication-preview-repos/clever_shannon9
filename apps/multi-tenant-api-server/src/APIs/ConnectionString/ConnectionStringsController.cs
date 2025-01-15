using Microsoft.AspNetCore.Mvc;

namespace MultiTenantApi.APIs;

[ApiController()]
public class ConnectionStringsController : ConnectionStringsControllerBase
{
    public ConnectionStringsController(IConnectionStringsService service)
        : base(service) { }
}
