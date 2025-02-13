using System.Linq.Expressions;
using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Queries;

public class FindTaskQuery : IRequest<IEnumerable<TaskEntity>>
{
    public Expression<Func<TaskEntity, bool>> Predicate { get; set; }
    public Guid UserId { get; set; }

    public FindTaskQuery(Expression<Func<TaskEntity, bool>> predicate, Guid userId)
    {
        Predicate = predicate;
        UserId = userId;
    }
}

public class FindTaskQueryHandler : IRequestHandler<FindTaskQuery, IEnumerable<TaskEntity>>
{
    private readonly IRepository<TaskEntity> _repository;

    public FindTaskQueryHandler(IRepository<TaskEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskEntity>> Handle(FindTaskQuery request, CancellationToken cancellationToken)
    {
        var userTasks = await _repository.FindAsync(t => t.UserId == request.UserId);
        return userTasks.Where(request.Predicate.Compile());
    }
}