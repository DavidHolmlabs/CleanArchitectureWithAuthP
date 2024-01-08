using AuthPermissions.AdminCode;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.AuthUsers.Queries.NavMenu;

public record NavMenuQuery : IRequest<NavMenuDto>
{
}

public class NavMenuQueryValidator : AbstractValidator<NavMenuQuery>
{
    public NavMenuQueryValidator()
    {
    }
}

public class NavMenuQueryHandler : IRequestHandler<NavMenuQuery, NavMenuDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IAuthUsersAdminService _authUsersAdmin;
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public NavMenuQueryHandler(IApplicationDbContext context, IUser user, IAuthUsersAdminService authUsersAdmin, IAuthTenantAdminService authTenantAdmin)
    {
        _context = context;
        _user = user;
        _authUsersAdmin = authUsersAdmin;
        _authTenantAdmin = authTenantAdmin;
    }

    public async Task<NavMenuDto> Handle(NavMenuQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _authUsersAdmin.FindAuthUserByUserIdAsync(_user.Id);

        NavMenuDto res = new();

        if (userResult.IsValid)
            res.AuthUser = userResult.Result;

        res.Tenant = await _authTenantAdmin.QueryTenants().FirstOrDefaultAsync(x => x.TenantId == _user.TenantId);

        return res;
    }
}
