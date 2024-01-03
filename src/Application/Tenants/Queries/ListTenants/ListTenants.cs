using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes;


namespace CleanArchitecture.Application.Tenants.Queries.ListTenants;

public record ListTenantsQuery : IRequest<List<TenantDto>>
{
}

public class ListTenantsQueryValidator : AbstractValidator<ListTenantsQuery>
{
    public ListTenantsQueryValidator()
    {
    }
}

public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, List<TenantDto>>
{
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public ListTenantsQueryHandler(IAuthTenantAdminService authTenantAdmin)
    {
        _authTenantAdmin = authTenantAdmin;
    }

    public async Task<List<TenantDto>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
    {
        List<Tenant> tenants = await _authTenantAdmin.QueryTenants().ToListAsync();
        return tenants.Select(tenant => new TenantDto
        {
            IsHierarchical = tenant.IsHierarchical,
            TenantId = tenant.TenantId,
            ParentDataKey = tenant.ParentDataKey,
            TenantFullName = tenant.TenantFullName
        }).ToList();
    }
}
