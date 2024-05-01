using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Tasks.Queries.GetAllTasks;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tags.Commands.DeleteTag;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var obj = await _context.Tags.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (obj is null) throw new ItemNotFoundException(request.Id);
        _context.Tags.Remove(obj);
        await _context.SaveChangesAsync(cancellationToken);
    }
}