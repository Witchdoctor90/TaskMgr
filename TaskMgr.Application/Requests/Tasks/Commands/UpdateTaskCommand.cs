using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Commands;

public class UpdateTaskCommand : IRequest<TaskEntity>
{
    public UpdateTaskCommand(TaskEntity task, Guid userId)
    {
        Task = task;
        UserId = userId;
    }

    public TaskEntity Task { get; set; }
    public Guid UserId { get; set; }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskEntity>
{
    private readonly IRepository<TaskEntity> _repository;

    public UpdateTaskCommandHandler(IRepository<TaskEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TaskEntity> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        if (request.Task.UserId != request.UserId) throw new UnauthorizedAccessException();
        if (await _repository.GetByIdAsync(request.Task.Id) is null)
            throw new TaskEntityNotFoundException(request.Task.Id);

        await _repository.UpdateAsync(request.Task);
        await _repository.SaveChangesAsync(cancellationToken);
        return request.Task;
    }
}