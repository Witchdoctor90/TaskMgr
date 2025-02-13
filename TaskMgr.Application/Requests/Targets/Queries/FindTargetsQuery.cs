using System.Linq.Expressions;
using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Queries;

public class FindTargetsQuery : IRequest<IEnumerable<TargetEntity>>
{
    public Guid UserId { get; set; }
    public Expression<Func<TargetEntity, bool>> Predicate { get; set; }

    public FindTargetsQuery(Guid userId, Expression<Func<TargetEntity, bool>> predicate)
    {
        UserId = userId;
        Predicate = predicate;
    }
}

public class FindTargetsQueryHandler : IRequestHandler<FindTargetsQuery, IEnumerable<TargetEntity>>
{
    private readonly IRepository<TargetEntity> _repository;

    public FindTargetsQueryHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TargetEntity>> Handle(FindTargetsQuery request, CancellationToken cancellationToken)
    {
        var userTargets = await _repository.FindAsync(t => t.UserId == request.UserId);
        return userTargets.Where(request.Predicate.Compile());
    }
}