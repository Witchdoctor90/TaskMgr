using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Targets.Commands.CreateTarget;


public class CreateTargetCommandHandler : IRequestHandler<CreateTargetCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateTargetCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTargetCommand request, CancellationToken cancellationToken)
    {
        var entity = new Target()
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            Tags = request.Tags
        };
        await _context.Targets.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}