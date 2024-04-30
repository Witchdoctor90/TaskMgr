using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Tags.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var obj = new Tag()
        {
            Id = request.Id,
            Title = request.Title
        };
        await _context.Tags.AddAsync(obj);
        await _context.SaveChangesAsync(cancellationToken);
        return obj.Id;
        
    }
}