namespace CleanArchitecture.Application.Tenants;
public class TenantDto
{
    public int TenantId { get; set; }

    public string ParentDataKey { get; set; } = "";

    public string TenantFullName { get; set; } = "";

    public bool IsHierarchical { get; set; }

}
