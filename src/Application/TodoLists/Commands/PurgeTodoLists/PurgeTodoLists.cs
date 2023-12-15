using AuthPermissions.AspNetCore;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Constants;

namespace CleanArchitecture.Application.TodoLists.Commands.PurgeTodoLists;


[HasPermission(Permissions.PurgeTodoLists)]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly ITenantDbContext _context;

    public PurgeTodoListsCommandHandler(ITenantDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoLists.RemoveRange(_context.TodoLists);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
