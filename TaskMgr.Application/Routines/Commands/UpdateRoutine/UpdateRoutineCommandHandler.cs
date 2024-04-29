using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommandHandler : IRequestHandler<UpdateRoutineCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateRoutineCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateRoutineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.Id);
        if (entity is null) throw new ItemNotFoundException(request.Id);
        entity.Title = request.Title;
        entity.Content = request.Content;
        entity.RelatedTarget = request.RelatedTarget;
        entity.TimeSpan = request.TimeSpan;
        entity.TimeCount = request.TimeCount;
        entity.StartTime = request.StartTime;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}