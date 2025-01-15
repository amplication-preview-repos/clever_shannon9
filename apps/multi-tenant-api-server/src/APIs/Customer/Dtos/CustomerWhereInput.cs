namespace MultiTenantApi.APIs.Dtos;

public class CustomerWhereInput
{
    public List<string>? ConnectionStrings { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<string>? UserTokens { get; set; }
}
