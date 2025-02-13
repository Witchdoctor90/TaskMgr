using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Commands;

public class DeleteTaskCommand : IRequest<Unit>
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }

    public DeleteTaskCommand(Guid taskId, Guid userId)
    {
        TaskId = taskId;
        UserId = userId;
    }
}

public  class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly IRepository<TaskEntity> _repository;

    public DeleteTaskCommandHandler(IRepository<TaskEntity> repository)
    {
        this._repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.TaskId);
        if(entity == null) throw new NullReferenceException();
        if(entity.UserId != request.UserId) throw new UnauthorizedAccessException();
        
        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}