using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Commands;

public class DeleteTargetCommand : IRequest<Unit>
{
    public Guid TargetId { get; set; }
    public Guid UserId { get; set; }

    public DeleteTargetCommand(Guid targetId, Guid userId)
    {
        TargetId = targetId;
        UserId = userId;
    }
}

public class DeleteTargetCommandHandler : IRequestHandler<DeleteTargetCommand, Unit>
{
    private readonly IRepository<TargetEntity> _repository;

    public DeleteTargetCommandHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTargetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.TargetId);
        if(entity?.UserId != request.UserId) throw new UnauthorizedAccessException();
        
        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}