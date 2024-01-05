using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Tenants;

namespace CleanArchitecture.Application.Orders.Queries.AvailableOrders;

public record AvailableOrdersQuery : IRequest<List<OrderDto>>
{
    public string? DataKey { get; set; }
}

public class AvailableOrdersQueryValidator : AbstractValidator<AvailableOrdersQuery>
{
    public AvailableOrdersQueryValidator()
    {
    }
}

public class AvailableOrdersQueryHandler : IRequestHandler<AvailableOrdersQuery, List<OrderDto>>
{
    private readonly INonTenantDbContext _context;
    private readonly IAuthTenantAdminService _authTenantAdmin;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public AvailableOrdersQueryHandler(
        INonTenantDbContext context,
        IAuthTenantAdminService authTenantAdmin,
        IUser user,
        IMapper mapper)
    {
        _context = context;
        _authTenantAdmin = authTenantAdmin;
        _user = user;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> Handle(AvailableOrdersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<string>? ids = _user.DataKey?.Split('.')?.Where(x => !string.IsNullOrEmpty(x));

        if (ids == null && request.DataKey != null)
            ids = request.DataKey.Split('.')?.Where(x => !string.IsNullOrEmpty(x));

        if (ids == null)
            return new List<OrderDto>();

        List<string> dataKeys = [];
        foreach (var id in ids)
        {
            var tenant = await _authTenantAdmin.GetTenantViaIdAsync(int.Parse(id));
            if (tenant.IsValid)
                dataKeys.Add(tenant.Result.GetTenantDataKey());
        }

        var orders = await _context.Orders
            .Where(x => x.DataKey != null && dataKeys.Contains(x.DataKey))
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var tenants = await _authTenantAdmin.QueryTenants().ToListAsync();

        foreach (var order in orders)
        {
            if (order.DataKey != null)
            {
                Tenant? tenant = tenants.FirstOrDefault(x => x.GetTenantDataKey() == order.DataKey);
                if (tenant != null)
                {
                    order.Tenant = _mapper.Map<TenantDto>(tenant);
                }
            }
        }

        return orders;
    }
}
