using System.Security.Claims;
using AuthPermissions.BaseCode.PermissionsCode;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? DataKey => _httpContextAccessor.HttpContext?.User?.FindFirstValue(PermissionConstants.DataKeyClaimType);
}
