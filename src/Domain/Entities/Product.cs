namespace CleanArchitecture.Domain.Entities;

public class Product : BaseAuditableEntity, INoDataKey
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}
