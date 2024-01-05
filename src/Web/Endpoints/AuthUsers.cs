using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.AuthUsers;
using CleanArchitecture.Application.AuthUsers.Queries.AuthUserInfo;
using CleanArchitecture.Application.AuthUsers.Queries.ListAuthUsers;

namespace CleanArchitecture.Web.Endpoints;

public class AuthUsers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAuthUsers)
            .MapGet(GetAuthUserInfo, "me");
    }

    public async Task<List<AuthUser>> GetAuthUsers(ISender sender)
    {
        return await sender.Send(new ListAuthUsersQuery());
    }

    public async Task<AuthUserInfoDto> GetAuthUserInfo(ISender sender)
    {
        return await sender.Send(new AuthUserInfoQuery());
    }
}
