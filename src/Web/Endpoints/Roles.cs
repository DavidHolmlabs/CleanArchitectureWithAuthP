using AuthPermissions.AdminCode;
using CleanArchitecture.Application.Roles.Queries.ListRoles;

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
