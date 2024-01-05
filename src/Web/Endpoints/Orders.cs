using CleanArchitecture.Application.Orders;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Queries.AvailableOrders;
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
            .MapGet(AvailableOrders, "available")
            .MapPost(CreateOrder);
    }

    public async Task<List<OrderDto>> GetOrders(ISender sender)
    {
        return await sender.Send(new ListOrdersQuery());
    }

    public async Task<List<OrderDto>> AvailableOrders(ISender sender)
    {
        return await sender.Send(new AvailableOrdersQuery());
    }

    public async Task<Order> CreateOrder(ISender sender, [Microsoft.AspNetCore.Mvc.FromBody] CreateOrderCommand command)
    {
        return await sender.Send(command);
    }
}
