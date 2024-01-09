using AuthPermissions.BaseCode.DataLayer.Classes;
using AuthPermissions.SupportCode.AddUsersServices;
using CleanArchitecture.Application.AuthUsers;
using CleanArchitecture.Application.AuthUsers.Commands.AcceptInvite;
using CleanArchitecture.Application.AuthUsers.Commands.InviteAuthUsers;
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
            .MapGet(GetAuthUserInfo, "me")
            .MapPost(AcceptInvite, "accept")
            .MapPost(Invite);
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

    public async Task<InviteDto> Invite(ISender sender, string email)
    {
        string baseUrl = "https://localhost:44447/accept-invite?verify=";

        var invite = await sender.Send(new InviteAuthUsersCommand { Email = email });

        invite.Url = baseUrl + invite.Url;

        return invite;
    }

    public async Task<AddNewUserDto> AcceptInvite(ISender sender, AcceptInviteCommand acceptInviteCommand)
    {
        StatusGeneric.IStatusGeneric<AddNewUserDto> status = await sender.Send(acceptInviteCommand);

        if (status.IsValid)
            return status.Result;

        return new AddNewUserDto();
    }
}
