using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Queries.ListOrders;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Web.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetOrders)
            .MapPost(CreateOrder);
    }

    public async Task<List<Order>> GetOrders(ISender sender)
    {
        return await sender.Send(new ListOrdersQuery());
    }

    public async Task<Order> CreateOrder(ISender sender, [Microsoft.AspNetCore.Mvc.FromBody] CreateOrderCommand command)
    {
        return await sender.Send(command);
    }
}
