using CleanArchitecture.Application.Tenants;
using CleanArchitecture.Application.Tenants.Queries.ListTenants;

namespace CleanArchitecture.Web.Endpoints;

public class Tenants : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTenants);
    }

    public async Task<List<TenantDto>> GetTenants(ISender sender)
    {
        return await sender.Send(new ListTenantsQuery());
    }
}
