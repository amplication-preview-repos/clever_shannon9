using MultiTenantApi.APIs;

namespace MultiTenantApi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IConnectionStringsService, ConnectionStringsService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IUserTokensService, UserTokensService>();
    }
}
