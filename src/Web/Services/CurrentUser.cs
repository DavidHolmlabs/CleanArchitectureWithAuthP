using System.Security.Claims;
using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.BaseCode.PermissionsCode;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILinkToTenantDataService _linkToTenantDataService;

    public CurrentUser(IHttpContextAccessor httpContextAccessor, ILinkToTenantDataService linkToTenantDataService)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkToTenantDataService = linkToTenantDataService;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? DataKey
    {
        get
        {
            string? linkedTenantDataKey = _linkToTenantDataService.GetDataKeyOfLinkedTenant();
            if (linkedTenantDataKey != null)
                return linkedTenantDataKey;

            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(PermissionConstants.DataKeyClaimType);
        }
    }

    public int? TenantId => int.Parse(DataKey?.Split('.').Last(x => !string.IsNullOrEmpty(x)) ?? "0");
}
