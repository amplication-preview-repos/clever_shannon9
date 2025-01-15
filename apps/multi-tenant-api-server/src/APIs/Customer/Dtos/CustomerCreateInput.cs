namespace MultiTenantApi.APIs.Dtos;

public class CustomerCreateInput
{
    public List<ConnectionString>? ConnectionStrings { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<UserToken>? UserTokens { get; set; }
}
