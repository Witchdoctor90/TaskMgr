using System.Linq.Expressions;
using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Queries;

public class FindRoutineQuery : IRequest<IEnumerable<RoutineEntity>>
{
    public Expression<Func<RoutineEntity, bool>> Predicate { get; set; }
    public Guid? UserId { get; set; }

    public FindRoutineQuery(Expression<Func<RoutineEntity, bool>> predicate, Guid? userId)
    {
        Predicate = predicate;
        UserId = userId;
    }
}

public class FindRoutineQueryHandler : IRequestHandler<FindRoutineQuery, IEnumerable<RoutineEntity>>
{
    private readonly IRepository<RoutineEntity> _repository;

    public FindRoutineQueryHandler(IRepository<RoutineEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoutineEntity>> Handle(FindRoutineQuery request, CancellationToken cancellationToken)
    {
        var userTasks = await _repository.FindAsync(t => t.UserId == request.UserId);
        return userTasks.Where(request.Predicate.Compile());
    }
}

