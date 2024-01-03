using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<Order>
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateOnly EndDate { get; set; }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly ITenantDbContext _context;

    public CreateOrderCommandHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Orders.Add(new Order
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            EndDate = request.EndDate,
        });

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity;
    }
}
