using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenantApi.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    public List<ConnectionStringDbModel>? ConnectionStrings { get; set; } =
        new List<ConnectionStringDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public List<UserTokenDbModel>? UserTokens { get; set; } = new List<UserTokenDbModel>();
}
