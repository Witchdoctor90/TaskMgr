using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var obj = await _context.Tags.FirstOrDefaultAsync(t => t.Id == request.Id);

        if (obj is null) throw new ItemNotFoundException(request.Id);
        
        obj.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);
        return obj.Id;
    }
}