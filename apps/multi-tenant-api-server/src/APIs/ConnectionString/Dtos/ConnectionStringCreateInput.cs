namespace MultiTenantApi.APIs.Dtos;

public class ConnectionStringCreateInput
{
    public string? ConnectionString { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}
