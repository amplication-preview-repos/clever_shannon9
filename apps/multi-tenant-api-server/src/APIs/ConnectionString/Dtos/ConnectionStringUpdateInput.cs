namespace MultiTenantApi.APIs.Dtos;

public class ConnectionStringUpdateInput
{
    public string? ConnectionString { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
