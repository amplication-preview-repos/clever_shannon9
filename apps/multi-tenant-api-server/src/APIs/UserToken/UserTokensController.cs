using Microsoft.AspNetCore.Mvc;

namespace MultiTenantApi.APIs;

[ApiController()]
public class UserTokensController : UserTokensControllerBase
{
    public UserTokensController(IUserTokensService service)
        : base(service) { }
}
