using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IAppDbContext _context;

    public UpdateTaskCommandHandler(IAppDbContext context)
    {
        _context = context; 
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (entity is null) throw new ItemNotFoundException(request.Id);
        entity.Title = request.Title;
        entity.Content = request.Content;
        entity.Status = request.Status;
        entity.RelatedTarget = request.RelatedTarget;
        entity.Tags = request.Tags;
        await _context.SaveChangesAsync(cancellationToken);
    }
}