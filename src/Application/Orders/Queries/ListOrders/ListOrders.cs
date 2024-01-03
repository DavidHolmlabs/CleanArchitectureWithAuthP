using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Queries.ListOrders;

public record ListOrdersQuery : IRequest<List<Order>>
{
}

public class ListOrdersQueryValidator : AbstractValidator<ListOrdersQuery>
{
    public ListOrdersQueryValidator()
    {
    }
}

public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, List<Order>>
{
    private readonly ITenantDbContext _context;

    public ListOrdersQueryHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders.ToListAsync();
    }
}
