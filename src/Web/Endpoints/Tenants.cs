using CleanArchitecture.Application.Tenants;
using CleanArchitecture.Application.Tenants.Commands.StartAccess;
using CleanArchitecture.Application.Tenants.Commands.StopAccess;
using CleanArchitecture.Application.Tenants.Queries.ListTenants;
using StatusGeneric;

namespace CleanArchitecture.Web.Endpoints;

public class Tenants : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTenants)
            .MapGet(StartAccess, "start-access")
            .MapGet(StopAccess, "stop-access");
    }

    public async Task<List<TenantDto>> GetTenants(ISender sender)
    {
        return await sender.Send(new ListTenantsQuery());
    }

    public async Task<IStatusGeneric> StartAccess(ISender sender, int tenantId)
    {
        return await sender.Send(new StartAccessCommand { TenantId = tenantId });
    }

    public async Task<IStatusGeneric> StopAccess(ISender sender)
    {
        return await sender.Send(new StopAccessCommand());
    }
}
