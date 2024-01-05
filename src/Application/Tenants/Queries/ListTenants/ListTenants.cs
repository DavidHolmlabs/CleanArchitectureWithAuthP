using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.Common.Interfaces;


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
    private readonly IUser _user;

    public ListTenantsQueryHandler(IAuthTenantAdminService authTenantAdmin, IUser user)
    {
        _authTenantAdmin = authTenantAdmin;
        _user = user;
    }

    public async Task<List<TenantDto>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
    {
        List<Tenant> tenants = await _authTenantAdmin
            .QueryTenants()
            .ToListAsync();

        var query = tenants.Select(tenant => new TenantDto
        {
            IsHierarchical = tenant.IsHierarchical,
            TenantId = tenant.TenantId,
            ParentDataKey = tenant.ParentDataKey,
            TenantFullName = tenant.TenantFullName,
            Name = tenant.GetTenantName(),
            DataKey = tenant.GetTenantDataKey()
        });

        if (_user.DataKey != null)
        {
            query = query.Where(x => x.DataKey.StartsWith(_user.DataKey));
        };

        return query.ToList();
    }
}
