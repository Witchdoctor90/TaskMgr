using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateTaskCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity()
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.Now,
            Tags = request.Tags,
            RelatedTarget = request.RelatedTarget,
            Status = Status.Created,
            StartTime = request.StartTime
        };
        var guid = await _context.Tasks.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}