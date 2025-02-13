using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Queries;

public class GetTargetByIdAsync : IRequest<TargetEntity>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public GetTargetByIdAsync(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}

public class GetTargetByIdAsyncHandler : IRequestHandler<GetTargetByIdAsync, TargetEntity>
{
    private readonly IRepository<TargetEntity> _repository;

    public GetTargetByIdAsyncHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TargetEntity> Handle(GetTargetByIdAsync request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity?.UserId != request.UserId) throw new UnauthorizedAccessException();
        return entity;
    }
}