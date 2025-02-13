using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Queries;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskEntity>>
{
    public Guid UserId { get; set; }

    public GetAllTasksQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskEntity>>
{
    private readonly IRepository<TaskEntity> _repository;

    public GetAllTasksQueryHandler(IRepository<TaskEntity> repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<TaskEntity>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.FindAsync(r => r.UserId == request.UserId);
    }
}