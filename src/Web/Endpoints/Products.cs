using CleanArchitecture.Application.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Products.Queries.ListProducts;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProducts)
            .MapPost(CreateProduct);
    }

    public async Task<List<Product>> GetProducts(ISender sender)
    {
        return await sender.Send(new ListProductsQuery());
    }

    public async Task<Product> CreateProduct(ISender sender, [Microsoft.AspNetCore.Mvc.FromBody] CreateProductCommand command)
    {
        return await sender.Send(command);
    }
}
