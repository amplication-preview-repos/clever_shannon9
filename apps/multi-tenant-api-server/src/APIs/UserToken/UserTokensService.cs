using MultiTenantApi.Infrastructure;

namespace MultiTenantApi.APIs;

public class UserTokensService : UserTokensServiceBase
{
    public UserTokensService(MultiTenantApiDbContext context)
        : base(context) { }
}
