using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.SupportCode.AddUsersServices;
using CleanArchitecture.Application.Common.Interfaces;
using StatusGeneric;

namespace CleanArchitecture.Application.Tenants.Commands.CreateDemo;

public record CreateDemoCommand : IRequest<IStatusGeneric>
{
    public string TenantName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public int ProductId { get; set; }
}

public class CreateDemoCommandValidator : AbstractValidator<CreateDemoCommand>
{
    public CreateDemoCommandValidator()
    {
    }
}

public class CreateDemoCommandHandler : IRequestHandler<CreateDemoCommand, IStatusGeneric>
{
    private readonly ITenantDbContext _context;
    private readonly IAuthTenantAdminService _authTenantAdmin;
    private readonly ISignInAndCreateTenant _userRegisterInvite;

    public CreateDemoCommandHandler(ITenantDbContext context, IAuthTenantAdminService authTenantAdmin, ISignInAndCreateTenant userRegisterInvite)
    {
        _context = context;
        _authTenantAdmin = authTenantAdmin;
        _userRegisterInvite = userRegisterInvite;
    }

    public async Task<IStatusGeneric> Handle(CreateDemoCommand request, CancellationToken cancellationToken)
    {
        var newUserData = new AddNewUserDto { Email = request.Email, Password = request.Password };
        var newTenantData = new AddNewTenantDto { TenantName = request.TenantName };
        IStatusGeneric<AddNewUserDto> status = await _userRegisterInvite.SignUpNewTenantWithVersionAsync(newUserData, newTenantData, new MultiTenantVersionData());

        DateOnly exp = DateOnly.FromDateTime(DateTime.Now.AddDays(5));

        if (status.IsValid)
        {
            var tenant = _authTenantAdmin.QueryTenants().Single(x => x.TenantId == status.Result.TenantId);

            _context.Orders.Add(new Domain.Entities.Order
            {
                ProductId = request.ProductId,
                Quantity = 2,
                EndDate = exp,
                DataKey = tenant.GetTenantDataKey()
            });

            await _context.SaveChangesAsync(cancellationToken);
        }

        return status;
    }
}
