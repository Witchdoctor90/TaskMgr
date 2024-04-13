using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Targets.Commands.DeleteTarget;

public class DeleteTargetCommandHandler : IRequestHandler<DeleteTargetCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTargetCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTargetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Targets.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (entity == null) throw new ItemNotFoundException(request.Id);
        _context.Targets.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}