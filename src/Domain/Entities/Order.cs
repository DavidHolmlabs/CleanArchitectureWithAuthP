using AuthPermissions.BaseCode.CommonCode;

namespace CleanArchitecture.Domain.Entities;

public class Order : BaseAuditableEntity, IDataKeyFilterReadWrite
{
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string? DataKey { get; set; }

    public int Quantity { get; set; }

    public DateOnly EndDate { get; set; }
}
