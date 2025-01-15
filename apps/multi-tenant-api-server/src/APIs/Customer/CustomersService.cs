using MultiTenantApi.Infrastructure;

namespace MultiTenantApi.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(MultiTenantApiDbContext context)
        : base(context) { }
}
