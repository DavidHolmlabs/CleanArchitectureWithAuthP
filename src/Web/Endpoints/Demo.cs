using CleanArchitecture.Application.Tenants.Commands.CreateDemo;
using StatusGeneric;

namespace CleanArchitecture.Web.Endpoints;

public class Demo : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateDemo);
    }

    public async Task<IStatusGeneric> CreateDemo(ISender sender, CreateDemoCommand createDemoCommand)
    {
        return await sender.Send(createDemoCommand);
    }
}
