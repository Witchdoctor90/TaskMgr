using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Commands;

public class DeleteTargetCommand : IRequest<bool>
{
    public DeleteTargetCommand(Guid targetId, Guid userId)
    {
        TargetId = targetId;
        UserId = userId;
    }

    public Guid TargetId { get; set; }
    public Guid UserId { get; set; }
}

public class DeleteTargetCommandHandler : IRequestHandler<DeleteTargetCommand, bool>
{
    private readonly IRepository<TargetEntity> _repository;

    public DeleteTargetCommandHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTargetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.TargetId);
        if (entity is null) throw new TaskEntityNotFoundException(request.TargetId);
        if (entity?.UserId != request.UserId) throw new UnauthorizedAccessException();

        await _repository.DeleteAsync(entity.Id);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}