using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.AuthUsers.Queries.ListAuthUsers;

namespace CleanArchitecture.Web.Endpoints;

public class AuthUsers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAuthUsers);
    }

    public async Task<List<AuthUser>> GetAuthUsers(ISender sender)
    {
        return await sender.Send(new ListAuthUsersQuery());
    }
}
