using AuthPermissions.SupportCode.AddUsersServices;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.AuthUsers.Commands.AcceptInvite;

public record AcceptInviteCommand : IRequest<StatusGeneric.IStatusGeneric<AddNewUserDto>>
{
    public string Verify { get; set; } = "";
    public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AcceptInviteCommandValidator : AbstractValidator<AcceptInviteCommand>
{
    public AcceptInviteCommandValidator()
    {
    }
}

public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, StatusGeneric.IStatusGeneric<AddNewUserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IInviteNewUserService _inviteUserServiceService;

    public AcceptInviteCommandHandler(IApplicationDbContext context, IInviteNewUserService inviteUserServiceService)
    {
        _context = context;
        _inviteUserServiceService = inviteUserServiceService;
    }

    public async Task<StatusGeneric.IStatusGeneric<AddNewUserDto>> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
    {
        return await _inviteUserServiceService.AddUserViaInvite(request.Verify, request.Email, null, request.Password, true);
    }
}
