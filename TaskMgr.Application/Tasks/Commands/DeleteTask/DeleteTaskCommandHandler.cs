using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tasks.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTaskCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (entity is null) throw new ItemNotFoundException(request.Id);
        _context.Tasks.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}