using AuthPermissions.BaseCode.CommonCode;

namespace CleanArchitecture.Domain.Entities;

public class TodoList : BaseAuditableEntity, IDataKeyFilterReadWrite, IDataKeyFilterReadOnly
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public string? DataKey { get; set; }

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
