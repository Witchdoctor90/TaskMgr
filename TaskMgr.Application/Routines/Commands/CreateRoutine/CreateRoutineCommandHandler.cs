using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Routines.Commands.CreateRoutine;

public class CreateRoutineCommandHandler : IRequestHandler<CreateRoutineCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateRoutineCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoutineCommand request, CancellationToken cancellationToken)
    {
        var entity = new Routine
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = request.CreatedAt,
            DeletedAt = request.DeletedAt,
            Id = request.Id,
            RelatedTarget = request.RelatedTarget,
            TimeSpan = request.TimeSpan,
            TimeCount = request.TimeCount,
            StartTime = request.StartTime,
            Tags = request.Tags,
        };
        await _context.Routines.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}