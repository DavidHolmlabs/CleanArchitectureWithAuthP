using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Products.Queries.ListProducts;

public record ListProductsQuery : IRequest<List<Product>>
{
}

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
    }
}

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, List<Product>>
{
    private readonly ITenantDbContext _context;

    public ListProductsQueryHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products.ToListAsync();
    }
}
