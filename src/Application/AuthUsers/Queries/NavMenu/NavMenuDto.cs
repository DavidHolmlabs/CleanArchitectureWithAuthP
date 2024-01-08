using AuthPermissions.BaseCode.DataLayer.Classes;

namespace CleanArchitecture.Application.AuthUsers.Queries.NavMenu;
public class NavMenuDto
{
    public Tenant? Tenant { get; set; }

    public AuthUser? AuthUser { get; set; }
}
