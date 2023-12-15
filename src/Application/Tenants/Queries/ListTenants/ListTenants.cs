using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes;


namespace CleanArchitecture.Application.Tenants.Queries.ListTenants;

public record ListTenantsQuery : IRequest<List<Tenant>>
{
}

public class ListTenantsQueryValidator : AbstractValidator<ListTenantsQuery>
{
    public ListTenantsQueryValidator()
    {
    }
}

public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, List<Tenant>>
{
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public ListTenantsQueryHandler(IAuthTenantAdminService authTenantAdmin)
    {
        _authTenantAdmin = authTenantAdmin;
    }

    public async Task<List<Tenant>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
    {
        List<Tenant> tenants = await _authTenantAdmin.QueryTenants().ToListAsync();
        return tenants;
    }
}
