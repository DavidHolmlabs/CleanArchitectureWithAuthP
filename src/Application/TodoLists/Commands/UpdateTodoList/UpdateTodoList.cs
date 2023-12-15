using AuthPermissions.AspNetCore;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Constants;

namespace CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;

[HasPermission(Permissions.UpdateTodoList)]
public record UpdateTodoListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly ITenantDbContext _context;

    public UpdateTodoListCommandHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
