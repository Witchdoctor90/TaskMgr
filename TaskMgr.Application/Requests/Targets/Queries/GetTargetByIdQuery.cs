using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Queries;

public class GetTargetByIdQuery : IRequest<TargetEntity>
{
    public GetTargetByIdQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}

public class GetTargetByIdQueryHandler : IRequestHandler<GetTargetByIdQuery, TargetEntity>
{
    private readonly IRepository<TargetEntity> _repository;

    public GetTargetByIdQueryHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TargetEntity> Handle(GetTargetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity is null) throw new TaskEntityNotFoundException(request.Id);
        if (entity?.UserId != request.UserId) throw new UnauthorizedAccessException();
        return entity;
    }
}