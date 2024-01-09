using AuthPermissions.SupportCode.AddUsersServices;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Graph;

namespace CleanArchitecture.Application.AuthUsers.Commands.InviteAuthUsers;

public record InviteAuthUsersCommand : IRequest<InviteDto>
{
    public string Email { get; set; } = "";
}

public class InviteAuthUsersCommandValidator : AbstractValidator<InviteAuthUsersCommand>
{
    public InviteAuthUsersCommandValidator()
    {
    }
}

public class InviteAuthUsersCommandHandler : IRequestHandler<InviteAuthUsersCommand, InviteDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IInviteNewUserService _inviteUserServiceService;
    private readonly IUser _user;

    public InviteAuthUsersCommandHandler(IApplicationDbContext context, IInviteNewUserService inviteUserServiceService, IUser user)
    {
        _context = context;
        _inviteUserServiceService = inviteUserServiceService;
        _user = user;
    }

    public async Task<InviteDto> Handle(InviteAuthUsersCommand request, CancellationToken cancellationToken)
    {
        var addUserData = new AddNewUserDto
        {
            Email = request.Email,
            Roles = ["Sales Assistant"],
            TenantId = _user.TenantId
        };

        var status = await _inviteUserServiceService.CreateInviteUserToJoinAsync(addUserData, _user.Id);

        if (status.IsValid)
        {
            return new()
            {
                Url = status.Result
            };
        }

        return new() { Url = status.GetAllErrors() };
    }
}
