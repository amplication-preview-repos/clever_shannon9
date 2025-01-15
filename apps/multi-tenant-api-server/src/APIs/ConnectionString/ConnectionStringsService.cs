using MultiTenantApi.Infrastructure;

namespace MultiTenantApi.APIs;

public class ConnectionStringsService : ConnectionStringsServiceBase
{
    public ConnectionStringsService(MultiTenantApiDbContext context)
        : base(context) { }
}
