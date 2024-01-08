using AuthPermissions.BaseCode.DataLayer.Classes;

namespace CleanArchitecture.Application.AuthUsers;
public class AuthUserInfoDto
{
    public string Email { get; set; } = "";

    public Dictionary<string, string> Claims { get; set; } = [];
    public string? DataKey { get; internal set; }
    public string? UserId { get; internal set; }
    public Tenant? Tenant { get; internal set; }
    public IReadOnlyCollection<UserToRole> Roles { get; internal set; } = [];
    public string Summary { get; internal set; } = "";
    public string Jwt { get; set; } = "";
}
