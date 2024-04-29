using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Routines.Commands.DeleteRoutine;

public class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand>
{
    private readonly IAppDbContext _context;

    public DeleteRoutineCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.Id);
        if (entity is null) throw new ItemNotFoundException(entity.Id);
        _context.Routines.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}