using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.CommonCode;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Tenants;

namespace CleanArchitecture.Application.Orders.Queries.ListOrders;

public record ListOrdersQuery : IRequest<List<OrderDto>>
{
}

public class ListOrdersQueryValidator : AbstractValidator<ListOrdersQuery>
{
    public ListOrdersQueryValidator()
    {
    }
}

public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, List<OrderDto>>
{
    private readonly ITenantDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthTenantAdminService _authTenantAdminService;

    public ListOrdersQueryHandler(ITenantDbContext context, IMapper mapper, IAuthTenantAdminService authTenantAdminService)
    {
        _context = context;
        _mapper = mapper;
        _authTenantAdminService = authTenantAdminService;
    }

    public async Task<List<OrderDto>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
    {
        Dictionary<string, TenantDto> tenants = await _authTenantAdminService
            .QueryTenants()
            .ToDictionaryAsync(x => x.GetTenantDataKey(), y => _mapper.Map<TenantDto>(y));

        var orderDtos = await _context
            .Orders
            .Include(x => x.Product)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var orderDto in orderDtos)
        {
            orderDto.Tenant = tenants[orderDto.DataKey ?? "missing"];
        }

        return orderDtos;
    }
}
