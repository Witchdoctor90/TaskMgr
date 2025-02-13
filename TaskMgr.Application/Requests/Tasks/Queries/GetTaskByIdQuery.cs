using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Queries;

public class GetTaskByIdQuery : IRequest<TaskEntity>
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }

    public GetTaskByIdQuery(Guid taskId)
    {
        TaskId = taskId;
    }
}

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskEntity>
{
    private readonly IRepository<TaskEntity> _repository;

    public GetTaskByIdQueryHandler(IRepository<TaskEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TaskEntity> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.TaskId);
        if(result?.UserId != request.UserId) throw new UnauthorizedAccessException();
        return result;
    }
}