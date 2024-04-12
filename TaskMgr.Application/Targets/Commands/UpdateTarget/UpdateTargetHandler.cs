using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Targets.Commands.UpdateTarget;

public class UpdateTargetHandler : IRequestHandler<UpdateTargetCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateTargetHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateTargetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Targets.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (entity == null) throw new ItemNotFoundException(request.Id);
        
        entity.Title = request.Title;
        entity.Content = request.Content;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}