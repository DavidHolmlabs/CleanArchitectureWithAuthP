using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly ITenantDbContext _context;

    public CreateProductCommandHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Products.Add(new Product
        {
            Name = request.Name,
            Description = request.Description,
        });

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity;
    }
}
