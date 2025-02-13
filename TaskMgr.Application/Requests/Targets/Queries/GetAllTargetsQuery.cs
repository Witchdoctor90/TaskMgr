using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Queries;

public class GetAllTargetsQuery : IRequest<IEnumerable<TargetEntity>>
{
    public Guid UserId { get; set; }

    public GetAllTargetsQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetAllTargetsQueryHandler : IRequestHandler<GetAllTargetsQuery, IEnumerable<TargetEntity>>
{
    private readonly IRepository<TargetEntity> _repository;

    public GetAllTargetsQueryHandler(IRepository<TargetEntity> repository)
    {
        this._repository = repository;
    }

    public async Task<IEnumerable<TargetEntity>> Handle(GetAllTargetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.FindAsync(r => r.UserId == request.UserId);
    }
}