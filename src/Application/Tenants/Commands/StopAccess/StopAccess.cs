using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.BaseCode.SetupCode;
using CleanArchitecture.Application.Common.Interfaces;
using LocalizeMessagesAndErrors;
using StatusGeneric;

namespace CleanArchitecture.Application.Tenants.Commands.StopAccess;

public record StopAccessCommand : IRequest<IStatusGeneric>
{
}

public class StopAccessCommandValidator : AbstractValidator<StopAccessCommand>
{
    public StopAccessCommandValidator()
    {
    }
}

public class StopAccessCommandHandler : IRequestHandler<StopAccessCommand, IStatusGeneric>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly ILinkToTenantDataService _linkToTenantDataService;
    private readonly IDefaultLocalizer _localizeDefault;

    public StopAccessCommandHandler(IApplicationDbContext context, IUser user, ILinkToTenantDataService linkToTenantDataService, IAuthPDefaultLocalizer localizeProvider)
    {
        _context = context;
        _user = user;
        _linkToTenantDataService = linkToTenantDataService;
        _localizeDefault = localizeProvider.DefaultLocalizer;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IStatusGeneric> Handle(StopAccessCommand request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        _linkToTenantDataService.StopLinkingToTenant();
        return new StatusGenericLocalizer(_localizeDefault);
    }
}
