using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.Roles.Queries.ListRoles;
using CleanArchitecture.Application.Tenants.Queries.ListTenants;

namespace CleanArchitecture.Web.Endpoints;

public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRoles);
    }

    public async Task<List<RoleWithPermissionNamesDto>> GetRoles(ISender sender)
    {
        return await sender.Send(new ListRolesQuery());
    }
}
