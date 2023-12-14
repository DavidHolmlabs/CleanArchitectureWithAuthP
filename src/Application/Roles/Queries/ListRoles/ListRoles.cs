using AuthPermissions.AdminCode;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Graph;

namespace CleanArchitecture.Application.Roles.Queries.ListRoles;

public record ListRolesQuery : IRequest<List<RoleWithPermissionNamesDto>>
{
}

public class ListRolesQueryValidator : AbstractValidator<ListRolesQuery>
{
    public ListRolesQueryValidator()
    {
    }
}

public class ListRolesQueryHandler : IRequestHandler<ListRolesQuery, List<RoleWithPermissionNamesDto>>
{
    private readonly IAuthRolesAdminService _authRolesAdmin;
    private readonly IUser _user;

    public ListRolesQueryHandler(IAuthRolesAdminService authRolesAdmin, IUser user)
    {
        _authRolesAdmin = authRolesAdmin;
        _user = user;
    }

    public async Task<List<RoleWithPermissionNamesDto>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
    {
        List<RoleWithPermissionNamesDto> permissionDisplay = await
            _authRolesAdmin.QueryRoleToPermissions(_user.Id)
                .OrderBy(x => x.RoleType)
                .ToListAsync();

        return permissionDisplay;
    }
}
