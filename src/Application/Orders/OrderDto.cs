using CleanArchitecture.Application.Tenants;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders;
public class OrderDto
{
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string? DataKey { get; set; }

    public TenantDto? Tenant { get; set; }

    public int Quantity { get; set; }

    public DateOnly EndDate { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
