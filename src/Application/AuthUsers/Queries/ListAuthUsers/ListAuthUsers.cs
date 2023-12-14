using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.AuthUsers.Queries.ListAuthUsers;

public record ListAuthUsersQuery : IRequest<List<AuthUser>>
{
}

public class ListAuthUsersQueryValidator : AbstractValidator<ListAuthUsersQuery>
{
    public ListAuthUsersQueryValidator()
    {
    }
}

public class ListAuthUsersQueryHandler : IRequestHandler<ListAuthUsersQuery, List<AuthUser>>
{
    private readonly IAuthUsersAdminService _authUsersAdmin;
    private readonly IUser _user;

    public ListAuthUsersQueryHandler(IAuthUsersAdminService authUsersAdmin, IUser user)
    {
        _authUsersAdmin = authUsersAdmin;
        _user = user;
    }

    public async Task<List<AuthUser>> Handle(ListAuthUsersQuery request, CancellationToken cancellationToken)
    {
        return await _authUsersAdmin.QueryAuthUsers(_user.DataKey).ToListAsync();
    }
}
