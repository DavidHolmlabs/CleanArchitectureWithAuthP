using System.Security.Claims;
using AuthPermissions.AdminCode;
using AuthPermissions;
using MediatR;
using CleanArchitecture.Application.Orders.Queries.AvailableOrders;
using AuthPermissions.BaseCode.CommonCode;

namespace CleanArchitecture.Infrastructure.Identity;
/// <summary>
/// This adds available product ids
/// </summary>
public class AddCertificationIdsClaims : IClaimsAdder
{
    public const string CertificationIdsClaimType = "CertificationIds";

    private readonly IAuthUsersAdminService _userAdmin;
    private readonly IMediator _mediator;

    public AddCertificationIdsClaims(IAuthUsersAdminService userAdmin, IMediator mediator)
    {
        _userAdmin = userAdmin;
        _mediator = mediator;
    }

    public async Task<Claim> AddClaimToUserAsync(string userId)
    {
        var user = (await _userAdmin.FindAuthUserByUserIdAsync(userId)).Result;

        var availableOrders = await _mediator.Send(new AvailableOrdersQuery { DataKey = user.UserTenant.GetTenantDataKey() });

        List<int> ids = [];

        foreach (var order in availableOrders)
        {
            ids.Add(order.ProductId);
        }

        ids = ids.Distinct().ToList();

        string idsAsString = ids.Aggregate<int, string>("", (tot, i) => $"{tot},{i}").TrimStart(',');

        return new Claim(CertificationIdsClaimType, idsAsString);
    }

    public static string? GetCertificationIdFromUser(ClaimsPrincipal user)
    {
        return user?.Claims.FirstOrDefault(x => x.Type == CertificationIdsClaimType)?.Value;
    }
}
