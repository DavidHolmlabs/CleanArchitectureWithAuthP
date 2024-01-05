using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthPermissions.AdminCode;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.AuthUsers.Queries.AuthUserInfo;

public record AuthUserInfoQuery : IRequest<AuthUserInfoDto>
{
}

public class AuthUserInfoQueryValidator : AbstractValidator<AuthUserInfoQuery>
{
    public AuthUserInfoQueryValidator()
    {
    }
}

public class AuthUserInfoQueryHandler : IRequestHandler<AuthUserInfoQuery, AuthUserInfoDto>
{
    private readonly IUser _user;
    private readonly IAuthUsersAdminService _authUsersAdmin;
    private readonly IIdentityService _identityService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthUserInfoQueryHandler(
        IUser user,
        IAuthUsersAdminService authUsersAdmin,
        IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
    {
        _user = user;
        _authUsersAdmin = authUsersAdmin;
        _identityService = identityService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthUserInfoDto> Handle(AuthUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (_user.Id == null)
            throw new InvalidOperationException();

        StatusGeneric.IStatusGeneric<AuthPermissions.BaseCode.DataLayer.Classes.AuthUser> authUser = await _authUsersAdmin.FindAuthUserByUserIdAsync(_user.Id);

        Dictionary<string, string> claims = _httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .ToDictionary(x => x.Type, y => y.Value) ?? [];

        return new AuthUserInfoDto
        {
            Email = authUser.Result.Email,
            DataKey = _user.DataKey,
            UserId = _user.Id,
            Tenant = authUser.Result.UserTenant,
            Roles = authUser.Result.UserRoles,
            Summary = authUser.Result.ToString(),
            Claims = claims,
            Jwt = GetJwt()
        };
    }

    private string GetJwt()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test test test test test alfa bravo charlie delta"));

        var token = new JwtSecurityToken(
            issuer: "silabs.se",
            audience: "tesus.se",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
