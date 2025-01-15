using Microsoft.EntityFrameworkCore;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.Infrastructure;

public class MultiTenantApiDbContext : DbContext
{
    public MultiTenantApiDbContext(DbContextOptions<MultiTenantApiDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<ConnectionStringDbModel> ConnectionStrings { get; set; }

    public DbSet<UserTokenDbModel> UserTokens { get; set; }
}
