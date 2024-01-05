using AuthPermissions.BaseCode.DataLayer.Classes;

namespace CleanArchitecture.Application.Tenants;
public class TenantDto
{
    public int TenantId { get; set; }

    public string DataKey { get; set; } = "";

    public string ParentDataKey { get; set; } = "";

    public string Name { get; set; } = "";

    public string TenantFullName { get; set; } = "";

    public bool IsHierarchical { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Tenant, TenantDto>();
        }
    }

}
