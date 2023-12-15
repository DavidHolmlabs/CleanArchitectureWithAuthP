using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;
public interface ITenantDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
