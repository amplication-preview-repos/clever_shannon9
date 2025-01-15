namespace MultiTenantApi.APIs.Dtos;

public class UserToken
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? Expiry { get; set; }

    public string Id { get; set; }

    public string? Token { get; set; }

    public DateTime UpdatedAt { get; set; }
}
