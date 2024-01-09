using AuthPermissions.AspNetCore.AccessTenantData;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Tenants.Commands.StartAccess;

public record StartAccessCommand : IRequest<StatusGeneric.IStatusGeneric>
{
    public int TenantId { get; set; }
}

public class StartAccessCommandValidator : AbstractValidator<StartAccessCommand>
{
    public StartAccessCommandValidator()
    {
    }
}

public class StartAccessCommandHandler : IRequestHandler<StartAccessCommand, StatusGeneric.IStatusGeneric>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly ILinkToTenantDataService _linkToTenantDataService;

    public StartAccessCommandHandler(IApplicationDbContext context, IUser user, ILinkToTenantDataService linkToTenantDataService)
    {
        _context = context;
        _user = user;
        _linkToTenantDataService = linkToTenantDataService;
    }

    public async Task<StatusGeneric.IStatusGeneric> Handle(StartAccessCommand request, CancellationToken cancellationToken)
    {
        return await _linkToTenantDataService.StartLinkingToTenantDataAsync(_user.Id, request.TenantId);
    }
}
