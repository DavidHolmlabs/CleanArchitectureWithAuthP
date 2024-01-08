using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.AuthUsers;
using CleanArchitecture.Application.AuthUsers.Queries.AuthUserInfo;
using CleanArchitecture.Application.AuthUsers.Queries.ListAuthUsers;
using CleanArchitecture.Application.AuthUsers.Queries.NavMenu;

namespace CleanArchitecture.Web.Endpoints;

public class AuthUsers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetAuthUsers)
            .MapGet(GetNavMenu, "nav-menu")
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

    public async Task<NavMenuDto> GetNavMenu(ISender sender)
    {
        return await sender.Send(new NavMenuQuery());
    }
}
